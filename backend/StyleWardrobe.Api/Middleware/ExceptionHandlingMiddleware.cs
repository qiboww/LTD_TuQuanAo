using System.Net;
using System.Text.Json;
using StyleWardrobe.Api.Common.Exceptions;
using StyleWardrobe.Api.DTOs.Common;

namespace StyleWardrobe.Api.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (AppException ex)
        {
            logger.LogWarning(ex, "Handled app exception: {Code} - {Message}", ex.Code, ex.Message);
            await HandleExceptionAsync(context, (int)ex.StatusCode, ex.Code, ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled server error occurred.");
            await HandleExceptionAsync(context, (int)HttpStatusCode.InternalServerError, "INTERNAL_ERROR", "An unexpected error occurred.");
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, int statusCode, string code, string message)
    {
        if (context.Response.HasStarted)
        {
            return;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = new ErrorResponse(code, message);
        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        await context.Response.WriteAsync(json);
    }
}
