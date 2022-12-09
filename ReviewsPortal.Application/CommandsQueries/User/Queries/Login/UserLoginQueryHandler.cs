using System.Globalization;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
        var user = await GetRegisteredUser(request);
        await CheckEnteredPassword(request, user);
        await _signInManager.SignInAsync(user, request.Remember);
        return Unit.Value;
    }

    private async Task<Domain.User> GetRegisteredUser(UserLoginQuery request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new UnfoundException(request.Email);
        }
        return user;
    }

    private async Task CheckEnteredPassword(UserLoginQuery request, Domain.User user)
    {
        var password = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!password)
        {
            throw new IncorrectPasswordException();
        }
    }
}