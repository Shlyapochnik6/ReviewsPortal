using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReviewsPortal.Application.Common.Clouds.Firebase;
using ReviewsPortal.Application.Interfaces;

namespace ReviewsPortal.Application.Common.Clouds;

public static class DependencyInjection
{
    public static void AddFirebaseCloud(this IServiceCollection services,
        IConfiguration configuration)
    {
        var bucketName = configuration["Firebase:Bucket"];
        var credentials = configuration.GetSection("Firebase")
            .Get<JsonCredentialParameters>();
        var storage = StorageClient
            .Create(GoogleCredential.FromJsonParameters(credentials));
        if (storage is null || bucketName is null)
            throw new NullReferenceException("Google cloud data loss");
        services.AddScoped<FirebaseCloud>(_ =>
            new FirebaseCloud(storage, bucketName));
    }
}