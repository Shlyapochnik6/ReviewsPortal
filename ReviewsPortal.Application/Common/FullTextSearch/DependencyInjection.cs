using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ReviewsPortal.Application.Common.FullTextSearch;

public static class DependencyInjection
{
    public static void AddAlgoliaSearchClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAlgoliaSearch(configuration);
        services.AddScoped<AlgoliaDbSync>();
    }

    private static void AddAlgoliaSearch(this IServiceCollection services,
        IConfiguration configuration)
    {
        var appId = configuration["Algolia:AppId"];
        var adminKey = configuration["Algolia:AdminKey"];
        var index = configuration["Algolia:Index"];
        if (appId == null || adminKey == null || index == null)
            throw new NullReferenceException("Missing keys to connect to Algolia");
        services.AddSingleton<AlgoliaClient>(_ => new AlgoliaClient(appId, adminKey, index));
    }
}