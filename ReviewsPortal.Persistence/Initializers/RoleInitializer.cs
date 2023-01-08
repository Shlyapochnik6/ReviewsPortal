using Microsoft.AspNetCore.Identity;
using ReviewsPortal.Application.Common.Consts;

namespace ReviewsPortal.Persistence.Initializers;

public class RoleInitializer
{
    private static readonly List<string> _roles = new() { Roles.Admin, Roles.User };
    
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public RoleInitializer(RoleManager<IdentityRole<Guid>> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task InitializeAsync()
    {
        foreach (var roleName in _roles)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                await _roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
        }
    }
}