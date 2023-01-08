using MediatR;
using Microsoft.AspNetCore.Authentication;

namespace ReviewsPortal.Application.CommandsQueries.ExternalLogin.Queries;

public class GetAuthenticationPropertiesQuery : IRequest<AuthenticationProperties>
{
    public string? Provider { get; set; }
    
    public string? RedirectUrl { get; set; }

    public GetAuthenticationPropertiesQuery(string provider, string redirectUrl)
    {
        Provider = provider;
        RedirectUrl = redirectUrl;
    }
}