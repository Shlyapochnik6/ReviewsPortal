using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Like.Commands.Set;

public class SetLikeCommand : IRequest<Guid>
{
    public Guid ReviewId { get; set; }
    
    public Guid? UserId { get; set; }
    
    public bool Status { get; set; }
}