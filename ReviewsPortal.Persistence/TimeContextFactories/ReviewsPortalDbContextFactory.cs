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
        var connection = GetConnectionString();
        var builder = new DbContextOptionsBuilder<ReviewsPortalDbContext>();
        builder.UseNpgsql(connection);
        return new ReviewsPortalDbContext(builder.Options);
    }

    private static string GetConnectionString()
    {
        var connectionString = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production"
            ? "Host=dpg-ce751q14rebdt3d6cbf0-a;Port=5432;Database=reviews_db;Username=reviews_db_user;Password=n67kVQ1jBwP1c208G3DbmxgFg2QIWLE7"
            : "Host=localhost;Port=5432;Database=ReviewsPortal;Username=postgres;Password=sa";
        return connectionString ?? throw new NullReferenceException("The connection string to database is empty");
    }
}