using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Movies.Common.Exceptions;
using Movies.Common.Wrappers;

namespace Movies.WebAPI.Filters;

 public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
    
    public ApiExceptionFilterAttribute()
    {
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(ValidationException), HandleValidationException },
            { typeof(NotFoundException), HandleNotFoundException },
            { typeof(BadRequestException), HandleBadRequestException }
        };
    }
    
    public override void OnException(ExceptionContext context)
    {
        HandleException(context);

        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        var type = context.Exception.GetType();

        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }

        if (!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context);
            return;
        }

        HandleUnknownException(context);
    }

    private static void HandleInvalidModelStateException(ExceptionContext context)
    {
        var details = new ExceptionDetails
        {
            Reference = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = "One or more validation failures have occurred.",
            StatusCode = 400,
            Message = context.ModelState
        };

        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }
    
    // StatusCode 500
    private static void HandleUnknownException(ExceptionContext context)
    {
        var details = new ExceptionDetails
        {
            Reference = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Title = "An error occurred while processing your request.",
            StatusCode = StatusCodes.Status500InternalServerError,
            Message = new Dictionary<string, string>
            {
                {"Source", context.Exception.Source},
                {"TargetSite", Convert.ToString(context.Exception.TargetSite)},
                {"Message", context.Exception.Message},
                {"File", Details(context.Exception)}
            }
        };

        context.Result = new ObjectResult(details) {StatusCode = StatusCodes.Status500InternalServerError};
        context.ExceptionHandled = true;
    }

    private static string Details(Exception ex)
    {
        var stackTrace = new StackTrace(ex, true);
        var frames = stackTrace.GetFrames();
        var traceStr = new StringBuilder();

        foreach (var frame in frames)
        {
            if(frame.GetFileLineNumber() < 1)
                continue;

            traceStr.Append($"FileName: {frame.GetFileName()!.Split("\\").Last()}");
            traceStr.Append($", LineNumber: {frame.GetFileLineNumber()}");
            traceStr.Append(' ');
        }

        return Convert.ToString(traceStr);
    }
    
    // StatusCode 400 - Fluent Validation
    private static void HandleValidationException(ExceptionContext context)
    {
        var exception = (ValidationException)context.Exception;

        var details = new ExceptionDetails
        {
            Reference = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title = "One or more validation failures have occurred.",
            StatusCode = 400,
            Message = exception.Errors
        };

        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }
    
    // StatusCode 400
    private static void HandleBadRequestException(ExceptionContext context)
    {
        var exception = (BadRequestException)context.Exception;

        var details = new ExceptionDetails
        {
            Reference = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "One or more validation failures have occurred.",
            StatusCode = 400,
            Message = exception.Message
        };

        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }

    // StatusCode 404
    private static void HandleNotFoundException(ExceptionContext context)
    {
        var exception = (NotFoundException)context.Exception;

        var details = new ExceptionDetails
        {
            Reference = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title = "The specified resource was not found.",
            StatusCode = 404,
            Message = exception.Message
        };

        context.Result = new NotFoundObjectResult(details);
        context.ExceptionHandled = true;
    }
}
