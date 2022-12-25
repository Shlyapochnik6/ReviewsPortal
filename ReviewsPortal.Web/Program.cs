using System.Reflection;
using Microsoft.AspNetCore.Identity;
using ReviewsPortal.Application;
using ReviewsPortal.Application.Common.Mappings;
using ReviewsPortal.Application.Interfaces;
using ReviewsPortal.Domain;
using ReviewsPortal.Persistence;
using ReviewsPortal.Persistence.Contexts;
using ReviewsPortal.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var policy = new CookiePolicyOptions()
{
    Secure = CookieSecurePolicy.Always
};

builder.Configuration.AddEnvironmentVariables()
    .AddUserSecrets(Assembly.GetExecutingAssembly(), true);
builder.Configuration
    .AddJsonFile("/etc/secrets/secrets.json", true);

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IReviewsPortalDbContext).Assembly));
});

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddPersistence();

builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 5;
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters =
        "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ" +
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";
    options.SignIn.RequireConfirmedEmail = true;
}).AddEntityFrameworkStores<ReviewsPortalDbContext>();

var app = builder.Build();

using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var provider = scope.ServiceProvider;
    try
    {
        var context = provider.GetRequiredService<ReviewsPortalDbContext>();
    }
    catch (Exception e)
    {
        throw;
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCookiePolicy(policy);
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
