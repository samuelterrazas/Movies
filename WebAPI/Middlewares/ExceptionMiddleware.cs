namespace Movies.WebAPI.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        switch (context.Response.StatusCode)
        {
            case StatusCodes.Status401Unauthorized:
                await UnauthorizedAccessException(context);
                break;
                
            case StatusCodes.Status403Forbidden:
                await ForbiddenAccessException(context);
                break;
        }
    }

    private static async Task UnauthorizedAccessException(HttpContext context)
    {
        context.Response.ContentType = "application/problem+json";

        var details = new ExceptionDetails
        (
            Reference: "https://tools.ietf.org/html/rfc7235#section-3.1",
            Title: "Unauthorized.",
            StatusCode: StatusCodes.Status401Unauthorized,
            Message: "You must be logged in to access the content."
        );
                    
        await JsonSerializer.SerializeAsync(context.Response.Body, details);
    }

    private static async Task ForbiddenAccessException(HttpContext context)
    {
        context.Response.ContentType = "application/problem+json";

        var details = new ExceptionDetails
        (
            Reference: "https://tools.ietf.org/html/rfc7231#section-6.5.3",
            Title: "Forbidden.",
            StatusCode: StatusCodes.Status403Forbidden,
            Message: "You are not allowed to access the content."
        );

        await JsonSerializer.SerializeAsync(context.Response.Body, details);
    }
}