using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ReviewsPortal.Application.Common.DbConnectionManagers;
using ReviewsPortal.Application.Interfaces;
using ReviewsPortal.Domain;
using ReviewsPortal.Persistence.Contexts;

namespace ReviewsPortal.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var connectionString = serviceProvider.GetRequiredService<DbConnectionSelection>().GetConnectionConfiguration();
        services.AddDbContext<ReviewsPortalDbContext>(options => options.UseNpgsql(connectionString,
            x => x.MigrationsAssembly(typeof(ReviewsPortalDbContext).Assembly.FullName)));
        services.AddScoped<IReviewsPortalDbContext, ReviewsPortalDbContext>();
        return services;
    }
}