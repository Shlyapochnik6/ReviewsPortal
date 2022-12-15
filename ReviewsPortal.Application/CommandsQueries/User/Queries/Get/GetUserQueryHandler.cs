using MediatR;
using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Application.Common.Exceptions;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.User.Queries.Get;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, Domain.User>
{
    private readonly IReviewsPortalDbContext _dbContext;

    public GetUserQueryHandler(IReviewsPortalDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Domain.User> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
        if (user == null)
            throw new NullReferenceException($"No user with id: {request.UserId} was found");
        return user;
    }
}