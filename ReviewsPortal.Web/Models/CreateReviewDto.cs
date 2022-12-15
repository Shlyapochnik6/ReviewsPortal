using AutoMapper;
using ReviewsPortal.Application.CommandsQueries.Review.Commands.Create;
using ReviewsPortal.Application.Common.Mappings;

namespace ReviewsPortal.Web.Models;

public class CreateReviewDto : IMapWith<CreateReviewCommand>
{
    public string Title { get; set; }

    public string ArtName { get; set; }

    public string Description { get; set; }

    public string Category { get; set; }

    public int Grade { get; set; }

    public string Tags { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateReviewDto, CreateReviewCommand>()
            .ForMember(r => r.Category,
                c => c.MapFrom(r => r.Category))
            .ForMember(r => r.Title,
                c => c.MapFrom(r => r.Title))
            .ForMember(r => r.Tags,
                c => c.MapFrom(r => r.Tags.Split(new[] { ',' })))
            .ForMember(r => r.Grade,
                c => c.MapFrom(r => r.Grade))
            .ForMember(r => r.Description,
                c => c.MapFrom(r => r.Description))
            .ForMember(r => r.ArtName,
                c => c.MapFrom(r => r.ArtName));
    }
}