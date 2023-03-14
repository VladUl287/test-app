using FluentValidation;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Presentation.Middleware;

internal sealed class ValidationExceptionHandlingMiddleware
{
    private readonly RequestDelegate next;

    public ValidationExceptionHandlingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, ValidationException exception)
    {
        var response = new
        {
            value = GetErrors(exception)
        };

        httpContext.Response.StatusCode = 400;
        httpContext.Response.ContentType = "application/json";

        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true
        };

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response, options));
    }

    private static string[] GetErrors(ValidationException exception)
    {
        return exception.Errors
            .Select(x => x.ErrorMessage)
            .ToArray();
    }
}