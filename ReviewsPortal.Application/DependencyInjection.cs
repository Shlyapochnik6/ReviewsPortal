using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReviewsPortal.Application.Common.Clouds;
using ReviewsPortal.Application.Common.DbConnectionManagers;
using ReviewsPortal.Application.Common.FullTextSearch;
using ReviewsPortal.Application.Common.OAuthConfigurations;
using ReviewsPortal.Application.Common.UserIdProviders;

namespace ReviewsPortal.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddConnectionStringConfiguration(configuration);
        services.AddExternalLoginConfiguration(configuration);
        services.AddAlgoliaSearchClient(configuration);
        services.AddFirebaseCloud(configuration);
        return services;
    }
    
    private static IServiceCollection AddExternalLoginConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddGoogleOAuthConfiguration(configuration)
            .AddMailRuOAuthConfiguration(configuration);
        return services;
    }
}