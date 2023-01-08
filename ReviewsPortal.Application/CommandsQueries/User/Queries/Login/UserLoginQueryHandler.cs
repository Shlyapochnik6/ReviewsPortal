using System.Globalization;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ReviewsPortal.Application.Common.Consts;
using ReviewsPortal.Application.Common.Exceptions;

namespace ReviewsPortal.Application.CommandsQueries.User.Queries.Login;

public class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, Unit>
{
    private readonly UserManager<Domain.User> _userManager;
    private readonly SignInManager<Domain.User> _signInManager;

    public UserLoginQueryHandler(UserManager<Domain.User> userManager, SignInManager<Domain.User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    public async Task<Unit> Handle(UserLoginQuery request, CancellationToken cancellationToken)
    {
        var user = await CheckUserAccessLevel(request);
        await _signInManager.SignInAsync(user, request.Remember);
        return Unit.Value;
    }

    private async Task<Domain.User> GetRegisteredUser(UserLoginQuery request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            throw new UnfoundException(request.Email);
        return user;
    }

    private async Task<Domain.User> CheckUserAccessLevel(UserLoginQuery request)
    {
        var user = await GetRegisteredUser(request);
        await CheckEnteredPassword(user, request.Password);
        if (user.AccessLevel == UserAccessStatuses.Blocked)
            throw new AccessDeniedException($"The user with id {user.Id} has been blocked!");
        return user;
    }

    private async Task CheckEnteredPassword(Domain.User user, string password)
    {
        var passwordCorrectness = await _userManager.CheckPasswordAsync(user, password);
        if (!passwordCorrectness)
            throw new IncorrectPasswordException();
    }
}