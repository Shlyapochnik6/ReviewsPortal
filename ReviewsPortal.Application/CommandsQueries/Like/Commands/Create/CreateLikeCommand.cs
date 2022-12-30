using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Like.Commands.Create;

public class CreateLikeCommand : IRequest<Domain.Like>
{
    public Guid ReviewId { get; set; }
    
    public Guid? UserId { get; set; }
    
    public bool Status { get; set; }
}