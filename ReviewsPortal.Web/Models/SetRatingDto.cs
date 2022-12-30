using AutoMapper;
using ReviewsPortal.Application.CommandsQueries.Rating.Commands.Create;
using ReviewsPortal.Application.CommandsQueries.Rating.Commands.Set;
using ReviewsPortal.Application.Common.Mappings;

namespace ReviewsPortal.Web.Models;

public class SetRatingDto : IMapWith<SetRatingCommand>
{
    public int Value { get; set; }
    
    public Guid? UserId { get; set; }
    
    public Guid ReviewId { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SetRatingDto, SetRatingCommand>()
            .ForMember(r => r.Value,
                opt => opt.MapFrom(r => r.Value))
            .ForMember(r => r.UserId,
                opt => opt.Ignore())
            .ForMember(r => r.ReviewId,
                opt => opt.MapFrom(r => r.ReviewId));
    }
}