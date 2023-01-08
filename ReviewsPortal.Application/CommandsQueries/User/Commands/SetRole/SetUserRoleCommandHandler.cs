using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Application.CommandsQueries.User.Queries.Get;
using ReviewsPortal.Application.Common.Exceptions;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.CommandsQueries.User.Commands.SetRole;

public class SetUserRoleCommandHandler : IRequestHandler<SetUserRoleCommand, Unit>
{
    private readonly IMediator _mediator;
    private readonly UserManager<Domain.User> _userManager;
    private readonly IReviewsPortalDbContext _dbContext;

    public SetUserRoleCommandHandler(IMediator mediator, UserManager<Domain.User> userManager,
        IReviewsPortalDbContext dbContext)
    {
        _mediator = mediator;
        _userManager = userManager;
        _dbContext = dbContext;
    }
    
    public async Task<Unit> Handle(SetUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await GetUser(_userManager.Users, request.UserId, cancellationToken);
        var userRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, userRoles);
        await _userManager.AddToRoleAsync(user, request.Role);
        return Unit.Value;
    }
    
    private async Task<Domain.User> GetUser(IQueryable<Domain.User> users, Guid userId,
        CancellationToken cancellationToken)
    {
        var user = await users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken)
                   ?? throw new UnfoundException($"The user with id {userId} is not found!");
        return user;
    }
}