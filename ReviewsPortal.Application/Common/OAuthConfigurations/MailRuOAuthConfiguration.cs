using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ReviewsPortal.Application.Common.OAuthConfigurations;

public static class MailRuOAuthConfiguration
{
    public static AuthenticationBuilder AddMailRuOAuthConfiguration(
        this AuthenticationBuilder authenticationBuilder, IConfiguration configuration)
    {
        authenticationBuilder.AddMailRu(options =>
        {
            options.ClientId = configuration["Authentication:MailRu:ClientId"] ?? string.Empty;
            options.ClientSecret = configuration["Authentication:MailRu:ClientSecret"] ?? string.Empty;
            options.SignInScheme = IdentityConstants.ExternalScheme;
            options.Scope.Add("email");
        });
        return authenticationBuilder;
    }
}