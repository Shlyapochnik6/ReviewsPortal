using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.User.Queries.Get;

public class GetUserQuery : IRequest<Domain.User>
{
    public Guid? UserId { get; set; }
}