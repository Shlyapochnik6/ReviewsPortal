namespace ReviewsPortal.Application.Common.DbConnectionManagers;

public interface IDbConnectionSelection
{
    string? GetConnectionConfiguration();
}