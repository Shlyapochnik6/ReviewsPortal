using Microsoft.Extensions.Configuration;

namespace ReviewsPortal.Application.Common.DbConnectionManagers;

public class DbConnectionSelection : IDbConnectionSelection
{
    public string? GetConnectionConfiguration()
    {
        var e = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var result = e == "Production" ? 
            "Host=dpg-ce751q14rebdt3d6cbf0-a;Port=5432;Database=reviews_db;Username=reviews_db_user;Password=n67kVQ1jBwP1c208G3DbmxgFg2QIWLE7" :
            "Host=localhost;Port=5432;Database=ReviewsPortal;Username=postgres;Password=sa";
        return result;
    }
}