using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.User.Commands.SetRole;

public class SetUserRoleCommand : IRequest<Unit>
{
    public Guid UserId { get; set; }
    public string Role { get; set; }

    public SetUserRoleCommand(Guid userId, string role)
    {
        UserId = userId;
        Role = role;
    }
}