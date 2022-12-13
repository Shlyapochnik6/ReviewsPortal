using System.Security.Policy;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace ReviewsPortal.Application.CommandsQueries.ExternalLogin.Queries;

public class ExternalLoginQueryHandler : IRequestHandler<ExternalLoginQuery, AuthenticationProperties>
{
    private readonly SignInManager<Domain.User> _signInManager;
    private readonly UserManager<Domain.User> _userManager;

    public ExternalLoginQueryHandler(SignInManager<Domain.User> signInManager,
        UserManager<Domain.User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public Task<AuthenticationProperties> Handle(ExternalLoginQuery request, CancellationToken cancellationToken)
    {
        var properties =
            _signInManager.ConfigureExternalAuthenticationProperties(request.Provider, request.RedirectUrl);
        return Task.FromResult(properties);
    }
}