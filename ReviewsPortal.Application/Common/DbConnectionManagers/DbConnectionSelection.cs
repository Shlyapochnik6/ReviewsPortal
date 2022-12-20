using Microsoft.Extensions.Configuration;

namespace ReviewsPortal.Application.Common.DbConnectionManagers;

public class DbConnectionSelection : IDbConnectionSelection
{
    public string? GetConnectionConfiguration()
    {
        var e = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var result = e == "Production" ? 
            "Host=dpg-cegthr82i3mkhvq8277g-a;Port=5432;Database=reviews_db_7bay;Username=kinelak;Password=K2e8pekfVBkBxEgjJX7lmuPZdrzPfRIf" :
            "Host=localhost;Port=5432;Database=ReviewsPortal;Username=postgres;Password=sa";
        return result;
    }
}