namespace ReviewsPortal.Web.Models;

public class UserRoleDto
{
    public string? Role { get; set; }

    public UserRoleDto(string? role)
    {
        Role = role;
    }
}