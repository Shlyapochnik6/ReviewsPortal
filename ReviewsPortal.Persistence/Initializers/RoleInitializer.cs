using Microsoft.AspNetCore.Identity;
using ReviewsPortal.Application.Common.Consts;

namespace ReviewsPortal.Persistence.Initializers;

public class RoleInitializer
{
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public RoleInitializer(RoleManager<IdentityRole<Guid>> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task InitializeAsync()
    {
        if (await _roleManager.RoleExistsAsync(Roles.Admin))
            return;
        await _roleManager.CreateAsync(new IdentityRole<Guid>(Roles.Admin));
    }
}