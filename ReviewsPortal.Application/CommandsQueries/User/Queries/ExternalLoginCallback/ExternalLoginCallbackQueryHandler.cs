using System.Security.Authentication;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ReviewsPortal.Application.CommandsQueries.User.Queries.ExternalLoginCallback;

public class ExternalLoginCallbackQueryHandler : IRequestHandler<ExternalLoginCallbackQuery, Unit>
{
    private readonly SignInManager<Domain.User> _signInManager;
    private readonly UserManager<Domain.User> _userManager;
    
    private const bool SaveCookies = false;

    public ExternalLoginCallbackQueryHandler(SignInManager<Domain.User> signInManager,
        UserManager<Domain.User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }
    
    public async Task<Unit> Handle(ExternalLoginCallbackQuery request, CancellationToken cancellationToken)
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        var login = await _signInManager.ExternalLoginSignInAsync(info!.LoginProvider, 
            info.ProviderKey, isPersistent: true);
        if (login.Succeeded)
            return Unit.Value;
        var user = await FindLocalUserByEmail(info) ?? await CreatLocalUser(info);
        await _userManager.AddLoginAsync(user, info);
        await _signInManager.SignInAsync(user, SaveCookies);
        return Unit.Value;
    }

    private async Task<Domain.User?> FindLocalUserByEmail(ExternalLoginInfo info)
    {
        var externalEmail = info.Principal.FindFirstValue(ClaimTypes.Email);
        if (externalEmail == null)
        {
            throw new AuthenticationException("The email address form provider is null!");
        }
        return await _userManager.FindByEmailAsync(externalEmail);
    }
    
    private async Task<Domain.User> CreatLocalUser(ExternalLoginInfo info)
    {
        var user = new Domain.User()
        {
            UserName = info.Principal.FindFirstValue(ClaimTypes.Name),
            Email = info.Principal.FindFirstValue(ClaimTypes.Email)
        };
        var result = await _userManager.CreateAsync(user);
        if (!result.Succeeded)
        {
            throw new AuthenticationException(result.Errors
                .Select(e => e.Description).Aggregate((errors, error) => $"{errors}, {error}"));
        }
        return user;
    }
}