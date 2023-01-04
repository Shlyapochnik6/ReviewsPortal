using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.Common.Clouds.Firebase;

public class FirebaseCloud
{
    private readonly string _bucketName;
    private readonly StorageClient _storage;

    public FirebaseCloud(StorageClient storage, string bucketName)
    {
        _storage = storage;
        _bucketName = bucketName;
    }

    public async Task<ImageData> UploadFile(IFormFile file, string folderName)
    {
        var filePath = $"{folderName}/{file.FileName}";
        var fileStream = await GetFileStream(file);
        await CreateRemoteFolder(folderName);
        var image = await _storage.UploadObjectAsync(_bucketName, filePath,
            file.ContentType, fileStream);
        return new ImageData(image.MediaLink, file.FileName, folderName);
    }

    public async Task<IEnumerable<ImageData>> UploadFiles(IEnumerable<IFormFile> files,
        string folderName)
    {
        var imagesData = new List<ImageData>();
        foreach (var file in files)
        {
            var imageData = await UploadFile(file, folderName);
            imagesData.Add(imageData);
        }
        return imagesData;
    }

    public async Task DeleteFolder(string? folderName)
    {
        if (folderName == null) return;
        var folders = _storage
            .ListObjectsAsync(_bucketName, folderName);
        await foreach (var folder in folders)
            await _storage.DeleteObjectAsync(_bucketName, folder.Name);
    }

    public async Task<IEnumerable<ImageData>> UpdateFiles(IEnumerable<IFormFile> files,
        string folderName)
    {
        var imagesData = new List<ImageData>();
        await DeleteFolder(folderName);
        foreach (var file in files)
        {
            var imageData = await UploadFile(file, folderName);
            imagesData.Add(imageData);
        }
        return imagesData;
    }

    private async Task<MemoryStream> GetFileStream(IFormFile file)
    {
        var fileStream = new MemoryStream();
        await file.CopyToAsync(fileStream);
        return fileStream;
    }

    private async Task CreateRemoteFolder(string folder)
    {
        const string fileMediaType = "application/x-directory";
        await _storage.UploadObjectAsync(_bucketName, $"{folder}/",
            fileMediaType, new MemoryStream());
    }
}