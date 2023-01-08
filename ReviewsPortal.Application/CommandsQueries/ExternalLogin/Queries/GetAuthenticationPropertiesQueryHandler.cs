using System.Security.Policy;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace ReviewsPortal.Application.CommandsQueries.ExternalLogin.Queries;

public class GetAuthenticationPropertiesQueryHandler : IRequestHandler<GetAuthenticationPropertiesQuery, AuthenticationProperties>
{
    private readonly SignInManager<Domain.User> _signInManager;
    private readonly UserManager<Domain.User> _userManager;

    public GetAuthenticationPropertiesQueryHandler(SignInManager<Domain.User> signInManager,
        UserManager<Domain.User> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public Task<AuthenticationProperties> Handle(GetAuthenticationPropertiesQuery request, CancellationToken cancellationToken)
    {
        var properties =
            _signInManager.ConfigureExternalAuthenticationProperties(request.Provider, request.RedirectUrl);
        return Task.FromResult(properties);
    }
}