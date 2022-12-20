using Microsoft.AspNetCore.Http;

namespace ReviewsPortal.Application.Common.Clouds.Mega;

public interface IMegaCloud
{
    Task<string> UploadFile(IFormFile file);
}