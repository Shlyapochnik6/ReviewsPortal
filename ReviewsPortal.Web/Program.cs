using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using ReviewsPortal.Application;
using ReviewsPortal.Application.Common.Hubs;
using ReviewsPortal.Application.Common.Mappings;
using ReviewsPortal.Application.Common.UserIdProviders;
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

builder.Services.AddSignalR();

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
await builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", corsPolicy =>
    {
        corsPolicy.AllowAnyHeader();
        corsPolicy.AllowAnyMethod();
        corsPolicy.AllowAnyOrigin();
    });
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseStaticFiles();
app.UseRouting();

app.UseCookiePolicy(policy);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapHub<CommentHub>("/hub-comment");

app.MapFallbackToFile("index.html");

app.Run();