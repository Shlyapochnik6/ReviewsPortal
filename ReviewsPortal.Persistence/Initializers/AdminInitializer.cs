using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ReviewsPortal.Application.Common.Consts;
using ReviewsPortal.Domain;

namespace ReviewsPortal.Persistence.Initializers;

public class AdminInitializer
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;

    public AdminInitializer(IConfiguration configuration,
        UserManager<User> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }

    public async Task InitializeAsync()
    {
        var adminUser = CreateAdminUser(_configuration);
        adminUser.AccessLevel = UserAccessStatuses.Unblocked;
        var admin = await _userManager.FindByEmailAsync(adminUser.Email!);
        if (admin == null)
        {
            admin = adminUser;
            await _userManager.CreateAsync(admin, adminUser.PasswordHash!);
        }
        await _userManager.AddToRoleAsync(admin, Roles.Admin);
    }

    private static User CreateAdminUser(IConfiguration configuration)
    {
        return new User()
        {
            Email = configuration["Admin:Email"],
            UserName = configuration["Admin:UserName"],
            PasswordHash = configuration["Admin:Password"]
        };
    }
}