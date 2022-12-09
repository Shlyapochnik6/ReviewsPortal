using System.Net;
using System.Text.Json;
using ReviewsPortal.Application.Common.Exceptions;

namespace ReviewsPortal.Web.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ExistingUserException e)
        {
            await HandleExceptionAsync(httpContext, e.Message,
                HttpStatusCode.Conflict);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(httpContext, e.Message,
                HttpStatusCode.NotFound);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context,
        string exMsg, HttpStatusCode httpStatusCode)
    {
        _logger.LogError(exMsg);
        HttpResponse response = await GenerateHttpResponseAsync(context, httpStatusCode);
        var errorDto = GenerateErrorDto(httpStatusCode, exMsg);
        string result = JsonSerializer.Serialize(errorDto);
        await response.WriteAsJsonAsync(result);
    }

    private Task<HttpResponse> GenerateHttpResponseAsync(HttpContext context,
        HttpStatusCode httpStatusCode)
    {
        HttpResponse response = context.Response;
        response.ContentType = "application/json";
        response.StatusCode = (int)httpStatusCode;
        return Task.FromResult(response);
    }

    private ErrorDto GenerateErrorDto(HttpStatusCode httpStatusCode, string exMsg)
    {
        var errorDto = new ErrorDto()
        {
            Message = exMsg,
            StatusCode = (int)httpStatusCode
        };
        return errorDto;
    }
}