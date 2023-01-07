using Microsoft.Extensions.Configuration;

namespace ReviewsPortal.Application.Common.DbConnectionManagers;

public class DbConnectionSelection : IDbConnectionSelection
{
    public string? GetConnectionConfiguration()
    {
        var e = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var result = e == "Production" ? 
            "Host=dpg-ceso85irrk0dan0pf4hg-a;Port=5432;Database=reviews_dmy5;Username=arseniy;Password=oxi8PQOI7rTRktnAL6ShRwwRaQ7s00MF" :
            "Host=localhost;Port=5432;Database=ReviewsPortal;Username=postgres;Password=sa";
        return result;
    }
}