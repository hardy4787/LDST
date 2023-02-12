using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace LDST.Api.Middlewares;

public class ValidationExceptionMiddleware
{
    private readonly IWebHostEnvironment _environment;
    private readonly RequestDelegate _next;
    private readonly ILogger<ValidationExceptionMiddleware> _logger;

    public ValidationExceptionMiddleware(
        RequestDelegate next,
        IWebHostEnvironment environment,
        ILogger<ValidationExceptionMiddleware> logger)
    {
        _next = next;
        _environment = environment;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        _logger.LogError(exception.ToString());

        if (_environment.IsDevelopment())
        {
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }

            var problemDetails = new ProblemDetails
            {
                Title = exception.Message,
                Status = context.Response.StatusCode,
            };
            return context.Response.WriteAsJsonAsync(problemDetails);
        }
        else
        {

            var problemDetails = new ProblemDetails
            {
                Title = "An error occured while processing your request.",
                Status = context.Response.StatusCode,
            };

            return context.Response.WriteAsJsonAsync(problemDetails);
        }

    }
}
