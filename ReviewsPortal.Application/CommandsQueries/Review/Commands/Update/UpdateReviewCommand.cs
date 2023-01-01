using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using ReviewsPortal.Application.Common.Mappings;

namespace ReviewsPortal.Application.CommandsQueries.Review.Commands.Update;

public class UpdateReviewCommand : IRequest, IMapWith<Domain.Review>
{
    public Guid ReviewId { get; set; }
    
    public string Title { get; set; }

    public string ArtName { get; set; }

    public string Description { get; set; }

    public IFormFile? ImageUrl { get; set; }

    public string CategoryName { get; set; }

    public int Grade { get; set; }

    public string[] Tags { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateReviewCommand, Domain.Review>()
            .ForMember(r => r.Title,
                opt => opt.MapFrom(r => r.Title))
            .ForMember(r => r.Description,
                opt => opt.MapFrom(r => r.Description))
            .ForMember(r => r.Grade,
                opt => opt.MapFrom(r => r.Grade))
            .ForMember(r => r.Tags,
                opt => opt.Ignore());
    }
}