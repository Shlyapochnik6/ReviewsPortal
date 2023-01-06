using System.Reflection;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReviewsPortal.Application.Common.DbConnectionManagers;
using ReviewsPortal.Persistence.Contexts;

namespace ReviewsPortal.Persistence.TimeContextFactories;

public class ReviewsPortalDbContextFactory : IDesignTimeDbContextFactory<ReviewsPortalDbContext>
{
    private readonly IServiceProvider _serviceProvider;
    private const string CurrentDirectoryName = "ReviewsPortal.Persistence";
    private const string MainDirectoryName = "ReviewsPortal.Web";
    
    public ReviewsPortalDbContextFactory() { }
    
    public ReviewsPortalDbContextFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ReviewsPortalDbContext CreateDbContext(string[] args)
    {
        var connection = GetConnectionString();
        var optionsBuilder = new DbContextOptionsBuilder<ReviewsPortalDbContext>();
        optionsBuilder.UseNpgsql(connection);
        return new ReviewsPortalDbContext(optionsBuilder.Options, _serviceProvider);
    }

    private static string GetConnectionString()
    {
        var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!
            .Replace(CurrentDirectoryName, MainDirectoryName);
        var configuration = new ConfigurationBuilder()
            .SetBasePath(directory)
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
            .AddJsonFile("/etc/secrets/secrets.json", true)
            .Build();
        var connectionString = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production"
            ? configuration["ProductionDbConnection"]
            : configuration["DbConnection"];
        return connectionString ?? throw new NullReferenceException("The connection string to database is empty");
    }
}