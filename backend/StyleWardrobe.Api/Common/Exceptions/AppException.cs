using System.Net;

namespace StyleWardrobe.Api.Common.Exceptions;

public class AppException(string code, string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : Exception(message)
{
    public string Code { get; } = code;
    public HttpStatusCode StatusCode { get; } = statusCode;
}

public class NotFoundException(string message = "Resource not found") 
    : AppException("NOT_FOUND", message, HttpStatusCode.NotFound);

public class ValidationException(string message) 
    : AppException("VALIDATION_ERROR", message, HttpStatusCode.BadRequest);

public class UnauthorizedException(string message = "Unauthorized") 
    : AppException("UNAUTHORIZED", message, HttpStatusCode.Unauthorized);
