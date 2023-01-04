using AutoMapper;
using ReviewsPortal.Application.Common.Mappings;
using ReviewsPortal.Domain;

namespace ReviewsPortal.Application.Common.Clouds.Firebase;

public class ImageData : IMapWith<Domain.Image>
{
    public string Url { get; set; }
    
    public string FileName { get; set; }
    
    public string FolderName { get; set; }

    public ImageData(params string[] fileData)
    {
        Url = fileData[0];
        FileName = fileData[1];
        FolderName = fileData[2];
    }

    public ImageData() { }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ImageData, Domain.Image>();
        profile.CreateMap<Domain.Image, ImageData>();
    }
}