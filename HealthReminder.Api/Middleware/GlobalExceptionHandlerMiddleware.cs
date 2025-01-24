using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception has occurred.");
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var statusCode = HttpStatusCode.InternalServerError;
        var errorCode = "E500";
        var message = "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde.";

        switch (exception)
        {
            case ArgumentNullException argumentNullException:
                statusCode = HttpStatusCode.BadRequest;
                errorCode = "E400";
                message = "Requisição inválida: " + argumentNullException.Message;
                break;
            case UnauthorizedAccessException unauthorizedAccessException:
                statusCode = HttpStatusCode.Unauthorized;
                errorCode = "E401";
                message = unauthorizedAccessException.Message;
                break;
            case KeyNotFoundException keyNotFoundException:
                statusCode = HttpStatusCode.NotFound;
                errorCode = "E404";
                message = "Recurso não encontrado: " + keyNotFoundException.Message;
                break;
            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                errorCode = "E404";
                message = notFoundException.Message;
                break;
            default:
                _logger.LogError(exception, "Unhandled exception occurred.");
                break;
        }

        context.Response.StatusCode = (int)statusCode;

        var errorResponse = new
        {
            errorCode,
            message,
            timestamp = DateTime.UtcNow,
            statusCode = (int)statusCode,
        };

        return context.Response.WriteAsJsonAsync(errorResponse);
    }

    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }
}