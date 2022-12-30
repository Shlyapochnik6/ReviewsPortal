using AutoMapper;
using ReviewsPortal.Application.CommandsQueries.Review.Commands.Update;
using ReviewsPortal.Application.Common.Mappings;

namespace ReviewsPortal.Web.Models;

public class UpdateReviewDto : IMapWith<UpdateReviewCommand>
{
    public Guid ReviewId { get; set; }
    
    public string Title { get; set; }

    public string ArtName { get; set; }

    public string Description { get; set; }

    public IFormFile? ImageUrl { get; set; }

    public string CategoryName { get; set; }

    public int Grade { get; set; }

    public string Tags { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateReviewDto, UpdateReviewCommand>()
            .ForMember(r => r.ReviewId,
                c => c.MapFrom(r => r.ReviewId))
            .ForMember(r => r.Title,
                c => c.MapFrom(r => r.Title))
            .ForMember(r => r.ArtName,
                c => c.MapFrom(r => r.ArtName))
            .ForMember(r => r.Description,
                c => c.MapFrom(r => r.Description))
            .ForMember(r => r.CategoryName,
                c => c.MapFrom(r => r.CategoryName))
            .ForMember(r => r.Grade,
                c => c.MapFrom(r => r.Grade))
            .ForMember(r => r.Tags,
                c => c.MapFrom(r => r.Tags.Split(new[] { ',' })))
            .ForMember(r => r.ImageUrl,
                opt => opt.MapFrom(r => r.ImageUrl));
    }
}