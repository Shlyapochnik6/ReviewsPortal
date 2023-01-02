using AutoMapper;
using Microsoft.AspNetCore.Http;
using ReviewsPortal.Application.Common.Mappings;

namespace ReviewsPortal.Application.CommandsQueries.Review.Queries.GetUpdated;

public class GetUpdatedReviewDto : IMapWith<Domain.Review>
{
    public string Title { get; set; }
    
    public string ArtName { get; set; }
    
    public string CategoryName { get; set; }

    public string[] Tags { get; set; }
    
    public string Description { get; set; }
    
    public IFormFile[]? Images { get; set; }

    public int Grade { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Review, GetUpdatedReviewDto>()
            .ForMember(r => r.Title,
                opt => opt.MapFrom(r => r.Title))
            .ForMember(r => r.ArtName,
                opt => opt.MapFrom(r => r.Art.ArtName))
            .ForMember(r => r.CategoryName,
                opt => opt.MapFrom(r => r.Category.CategoryName))
            .ForMember(r => r.Description,
                opt => opt.MapFrom(r => r.Description))
            .ForMember(r => r.Tags,
                opt => opt.MapFrom(r => r.Tags.Select(t => t.TagName)))
            .ForMember(r => r.Images,
                opt => opt.MapFrom(r => r.Images
                    .Select(i => i.Url)))
            .ForMember(r => r.Grade,
                opt => opt.MapFrom(r => r.Grade));
    }
}