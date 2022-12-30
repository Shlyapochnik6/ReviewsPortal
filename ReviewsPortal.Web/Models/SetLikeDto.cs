using AutoMapper;
using ReviewsPortal.Application.CommandsQueries.Like.Commands.Set;
using ReviewsPortal.Application.Common.Mappings;

namespace ReviewsPortal.Web.Models;

public class SetLikeDto : IMapWith<SetLikeCommand>
{
    public Guid ReviewId { get; set; }
    
    public Guid? UserId { get; set; }

    public bool Status { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SetLikeDto, SetLikeCommand>()
            .ForMember(l => l.ReviewId,
                opt => opt.MapFrom(l => l.ReviewId))
            .ForMember(l => l.UserId,
                opt => opt.Ignore())
            .ForMember(l => l.Status,
                opt => opt.MapFrom(l => l.Status));
    }
}