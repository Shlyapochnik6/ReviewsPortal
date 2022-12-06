using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReviewsPortal.Application.Common.DbConnectionManagers;
using ReviewsPortal.Persistence.Contexts;

namespace ReviewsPortal.Persistence.TimeContextFactories;

public class ReviewsPortalDbContextFactory : IDesignTimeDbContextFactory<ReviewsPortalDbContext>
{
    public ReviewsPortalDbContext CreateDbContext(string[] args)
    {
        var connectionString = new DbConnectionSelection().GetConnectionConfiguration();
        var builder = new DbContextOptionsBuilder<ReviewsPortalDbContext>();
        builder.UseNpgsql(connectionString);
        return new ReviewsPortalDbContext(builder.Options);
    }
}