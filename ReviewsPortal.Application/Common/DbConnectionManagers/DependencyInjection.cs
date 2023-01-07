using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ReviewsPortal.Application.Common.DbConnectionManagers;

public static class DependencyInjection
{
    public static void AddConnectionStringConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IDbConnectionSelection, DbConnectionSelection>(_ =>
            new DbConnectionSelection(configuration));
    }
}