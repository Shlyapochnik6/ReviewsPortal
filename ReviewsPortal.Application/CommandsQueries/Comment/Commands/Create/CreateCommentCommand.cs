using AutoMapper;
using MediatR;
using ReviewsPortal.Application.Common.Mappings;

namespace ReviewsPortal.Application.CommandsQueries.Comment.Commands.Create;

public class CreateCommentCommand : IRequest<Guid>, IMapWith<Domain.Comment>
{
    public string Text { get; set; }

    public Guid? UserId { get; set; }

    public Guid ReviewId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCommentCommand, Domain.Comment>()
            .ForMember(c => c.Text,
                opt => opt.MapFrom(c => c.Text))
            .ForMember(c => c.CreationDate,
                opt =>  opt.MapFrom(c => DateTime.UtcNow));
    }
}