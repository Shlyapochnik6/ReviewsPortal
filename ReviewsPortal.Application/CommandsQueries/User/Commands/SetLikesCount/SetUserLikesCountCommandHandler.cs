using MediatR;
using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Application.CommandsQueries.User.Queries.Get;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.User.Commands.SetLikesCount;

public class SetUserLikesCountCommandHandler : IRequestHandler<SetUserLikesCountCommand, Unit>
{
    private readonly IMediator _mediator;
    private readonly IReviewsPortalDbContext _dbContext;

    public SetUserLikesCountCommandHandler(IReviewsPortalDbContext dbContext, IMediator mediator)
    {
        _mediator = mediator;
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(SetUserLikesCountCommand request, CancellationToken cancellationToken)
    {
        var user = await GetUser(request.UserId, cancellationToken);
        user.LikesCount = GetCurrentLikesCount(user);
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }

    private async Task<Domain.User> GetUser(Guid? userId, CancellationToken cancellationToken)
    {
        var query = new GetUserQuery() { UserId = userId };
        return await _mediator.Send(query, cancellationToken);
    }

    private int GetCurrentLikesCount(Domain.User user)
    {
        var count = _dbContext.Likes
            .Where(l => l.User.Id == user.Id).Count(l => l.Status == true);
        return count;
    }
}