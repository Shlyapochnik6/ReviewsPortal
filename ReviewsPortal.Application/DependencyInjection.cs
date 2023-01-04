using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReviewsPortal.Application.Common.Clouds;
using ReviewsPortal.Application.Common.DbConnectionManagers;
using ReviewsPortal.Application.Common.UserIdProviders;

namespace ReviewsPortal.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddConnectionManager();
        services.AddExternalLoginConfiguration(configuration);
        services.AddFirebaseCloud(configuration);
        return services;
    }

    private static void AddConnectionManager(this IServiceCollection services)
    {
        services.AddScoped<IDbConnectionSelection, DbConnectionSelection>(_ =>
            new DbConnectionSelection());
    }
    
    private static IServiceCollection AddExternalLoginConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignOutScheme = IdentityConstants.ApplicationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/login";
            })
            .AddGoogle(options =>
            {
                options.ClientId = configuration["Authentication:Google:ClientId"] ?? string.Empty;
                options.ClientSecret = configuration["Authentication:Google:ClientSecret"] ?? string.Empty;
            })
            .AddMailRu(options =>
            {
                options.ClientId = configuration["Authentication:MailRu:ClientId"] ?? string.Empty;
                options.ClientSecret = configuration["Authentication:MailRu:ClientSecret"] ?? string.Empty;
            });
        return services;
    }
}