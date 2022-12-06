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
var configuration = builder.Configuration;

builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IReviewsPortalDbContext).Assembly));
});

builder.Services.AddApplication(configuration);
builder.Services.AddPersistence();

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
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

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
