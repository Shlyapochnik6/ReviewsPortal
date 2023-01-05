using MediatR;

namespace ReviewsPortal.Application.CommandsQueries.User.Queries.GetAll;

public class GetAllUsersQuery : IRequest<IEnumerable<GetAllUsersDto>>
{
    
}