using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.User.Queries.Login;

public class UserLoginQuery : IRequest
{
    public string Email { get; set; }
    
    public string Password { get; set; }

    public bool Remember { get; set; }
}