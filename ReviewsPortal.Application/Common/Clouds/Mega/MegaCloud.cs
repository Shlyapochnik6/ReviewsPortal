using CG.Web.MegaApiClient;
using Microsoft.AspNetCore.Http;

namespace ReviewsPortal.Application.Common.Clouds.Mega;

public class MegaCloud : IMegaCloud
{
    private readonly MegaApiClient _client;
    private readonly Guid _folderName;
    private const string BaseCloudPath = "RPimages";
    
    public MegaCloud(string email, string password)
    {
        _client = new MegaApiClient();
        _client.Login(email, password);
        _folderName = Guid.NewGuid();
    }
    
    public async Task<string> UploadFile(IFormFile file)
    {
        var nodes = await _client.GetNodesAsync();
        var root = nodes.Single(x => x.Name == BaseCloudPath);
        var folder = await CreateFolder(_folderName.ToString(), root);
        var stream = await GetFileStream(file);
        var fileNode = await _client.UploadAsync(stream, file.FileName, folder);
        var uri = await _client.GetDownloadLinkAsync(fileNode);
        return uri.AbsoluteUri;
    }

    private async Task<INode> CreateFolder(string name, INode node)
    {
        var folder = await _client.CreateFolderAsync(name, node);
        if (folder == null)
            throw new NullReferenceException("Failed to create folder");
        return folder;
    }

    private static async Task<Stream> GetFileStream(IFormFile file)
    {
        var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        return stream;
    }
}