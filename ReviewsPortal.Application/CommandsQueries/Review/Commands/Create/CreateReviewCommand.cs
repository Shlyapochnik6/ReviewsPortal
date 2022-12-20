using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using ReviewsPortal.Application.Common.Mappings;

namespace ReviewsPortal.Application.CommandsQueries.Review.Commands.Create;

public class CreateReviewCommand : IRequest<Guid>, IMapWith<Domain.Review>
{
    public Guid? UserId { get; set; }
    
    public string Title { get; set; }

    public string ArtName { get; set; }

    public string Description { get; set; }

    public IFormFile ImageUrl { get; set; }

    public string Category { get; set; }

    public int Grade { get; set; }

    public string[] Tags { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateReviewCommand, Domain.Review>()
            .ForMember(u => u.Title,
                opt => opt.MapFrom(u => u.Title))
            .ForMember(u => u.Category,
                opt => opt.MapFrom(u => u.Category))
            .ForMember(u => u.Description,
                opt => opt.MapFrom(u => u.Description))
            .ForMember(u => u.ImageUrl,
                opt => opt.MapFrom(u => u.ImageUrl))
            .ForMember(u => u.Grade,
                opt => opt.MapFrom(u => u.Grade))
            .ForMember(u => u.Tags,
                opt => opt.Ignore())
            .ForMember(u => u.Category,
                opt => opt.Ignore());
    }
}