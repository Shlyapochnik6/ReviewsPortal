using AutoMapper;
using ReviewsPortal.Application.CommandsQueries.Comment.Commands.Create;
using ReviewsPortal.Application.Common.Mappings;

namespace ReviewsPortal.Web.Models;

public class CreateCommentDto : IMapWith<CreateCommentCommand>
{
    public string Text { get; set; }
    
    public Guid? UserId { get; set; }

    public Guid ReviewId { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCommentDto, CreateCommentCommand>()
            .ForMember(c => c.ReviewId,
                opt => opt.MapFrom(c => c.ReviewId))
            .ForMember(c => c.Text,
                opt => opt.MapFrom(c => c.Text));
    }
}