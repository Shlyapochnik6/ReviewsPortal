using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.Like.Commands.Create;

public class CreateLikeCommand : IRequest<Domain.Like>
{
    public Guid ReviewId { get; set; }
    
    public Guid? UserId { get; set; }
    
    public bool Status { get; set; }

    public CreateLikeCommand(Guid reviewId, Guid? userId,
        bool status)
    {
        ReviewId = reviewId;
        UserId = userId;
        Status = status;
    }
}