using Grpc.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReviewsPortal.Application.Common.DbConnectionManagers;
using ReviewsPortal.Application.Interfaces;
using ReviewsPortal.Domain;
using ReviewsPortal.Persistence.Contexts;
using ReviewsPortal.Persistence.Initializers;

namespace ReviewsPortal.Persistence;

public static class DependencyInjection
{
    public static async Task<IServiceCollection> AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        var serviceProvider = services.BuildServiceProvider();
        var connectionSelection = serviceProvider.GetRequiredService<IDbConnectionSelection>();
        var connectionString = connectionSelection.GetConnectionConfiguration();
        services.AddDbContext<ReviewsPortalDbContext>(options =>
            options.UseNpgsql(connectionString, x =>
            {
                x.MigrationsAssembly(typeof(ReviewsPortalDbContext).Assembly.FullName);
                x.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            }));
        services.AddScoped<IReviewsPortalDbContext, ReviewsPortalDbContext>();
        services.AddIdentityConfiguration();
        services.AddCookieConfiguration();
        await services.AddRoleBasedAuthorization(configuration);
        return services;
    }

    private static void AddCookieConfiguration(this IServiceCollection services)
    {
        Task<int> RedirectBehavior(BaseContext<CookieAuthenticationOptions> context,
            int statusCode)
        {
            context.Response.Clear();
            context.Response.StatusCode = statusCode;
            return Task.FromResult(0);
        }
        services.ConfigureApplicationCookie(options =>
        {
            options.Events = new CookieAuthenticationEvents()
            {
                OnRedirectToLogin = context => RedirectBehavior(context, 401),
                OnRedirectToAccessDenied = context => RedirectBehavior(context, 403)
            };
        });
    }

    private static void AddIdentityConfiguration(this IServiceCollection services)
    {
        services.AddIdentity<User, IdentityRole<Guid>>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 5;
            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters = null!;
            options.SignIn.RequireConfirmedEmail = true;
        }).AddEntityFrameworkStores<ReviewsPortalDbContext>();
    }

    private static async Task AddRoleBasedAuthorization(this IServiceCollection services,
        IConfiguration configuration)
    {
        var serviceProvider = services.BuildServiceProvider();
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var roleManager = serviceProvider
            .GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        await new RoleInitializer(roleManager).InitializeAsync();
        await new AdminInitializer(configuration, userManager).InitializeAsync();
    } 
}