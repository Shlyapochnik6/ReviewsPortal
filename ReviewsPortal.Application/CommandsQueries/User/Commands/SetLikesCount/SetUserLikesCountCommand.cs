using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.User.Commands.SetLikesCount;

public class SetUserLikesCountCommand : IRequest
{
    public Guid? UserId { get; set; }

    public SetUserLikesCountCommand(Guid? userId)
    {
        UserId = userId;
    }
}