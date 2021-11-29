using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Movies.WebAPI.Middlewares
{
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

            var problem = new ProblemDetails
            {
                Title = "Unauthorized.",
                Status = StatusCodes.Status401Unauthorized,
                Detail = "https://tools.ietf.org/html/rfc7235#section-3.1"
            };
                    
            await JsonSerializer.SerializeAsync(context.Response.Body, problem);
        }

        private static async Task ForbiddenAccessException(HttpContext context)
        {
            context.Response.ContentType = "application/problem+json";

            var problem = new ProblemDetails
            {
                Title = "Forbidden.",
                Status = StatusCodes.Status403Forbidden,
                Detail = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
            };

            await JsonSerializer.SerializeAsync(context.Response.Body, problem);
        }
    }
}