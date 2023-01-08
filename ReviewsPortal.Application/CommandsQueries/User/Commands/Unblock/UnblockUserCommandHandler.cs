﻿using MediatR;
using ReviewsPortal.Application.CommandsQueries.User.Queries.Get;
using ReviewsPortal.Application.Common.Consts;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.User.Commands.Unblock;

public class UnblockUserCommandHandler : IRequestHandler<UnblockUserCommand, Unit>
{
    private readonly IMediator _mediator;
    private readonly IReviewsPortalDbContext _dbContext;

    public UnblockUserCommandHandler(IMediator mediator,
        IReviewsPortalDbContext dbContext)
    {
        _mediator = mediator;
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(UnblockUserCommand request, CancellationToken cancellationToken)
    {
        var user = await GetUser(request.UserId);
        user.AccessLevel = UserAccessStatuses.Unblocked;
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
    
    private async Task<Domain.User> GetUser(Guid userId)
    {
        var query = new GetUserQuery(userId);
        return await _mediator.Send(query);
    }
}