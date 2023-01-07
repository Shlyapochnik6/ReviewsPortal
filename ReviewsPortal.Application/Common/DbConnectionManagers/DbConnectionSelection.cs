using Microsoft.Extensions.Configuration;

namespace ReviewsPortal.Application.Common.DbConnectionManagers;

public class DbConnectionSelection : IDbConnectionSelection
{
    private readonly IConfiguration _configuration;

    public DbConnectionSelection(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string? GetConnectionConfiguration()
    {
        var e = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var result = e == "Production" ? 
            _configuration["ProductionDbConnection"] :
            _configuration["DbConnection"];
        return result;
    }
}