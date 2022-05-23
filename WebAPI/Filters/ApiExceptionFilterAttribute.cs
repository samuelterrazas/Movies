namespace Movies.WebAPI.Filters;

 public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
    
    public ApiExceptionFilterAttribute()
    {
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(ValidationException), HandleValidationException },
            { typeof(BadRequestException), HandleBadRequestException },
            { typeof(NotFoundException), HandleNotFoundException }
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
    
    // StatusCode 400 - InvalidModelState
    private static void HandleInvalidModelStateException(ExceptionContext context)
    {
        var details = new ExceptionDetails
        (
            Reference: "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title: "One or more validation failures have occurred.",
            StatusCode: StatusCodes.Status400BadRequest,
            Message: context.ModelState
        );

        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }
    
    // StatusCode 500 - InternalServerError
    private static void HandleUnknownException(ExceptionContext context)
    {
        var details = new ExceptionDetails
        (
            Reference: "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Title: "An error occurred while processing your request.",
            StatusCode: StatusCodes.Status500InternalServerError,
            Message: new Dictionary<string, string>
            {
                {"Source", context.Exception.Source!},
                {"TargetSite", Convert.ToString(context.Exception.TargetSite)!},
                {"Message", context.Exception.Message},
                {"File", Details(context.Exception)}
            }
        );

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

            traceStr.AppendLine($"FileName: {frame.GetFileName()!.Split("\\").Last()}");
            traceStr.AppendLine($", LineNumber: {frame.GetFileLineNumber()}");
            traceStr.Append(' ');
        }

        return Convert.ToString(traceStr)!;
    }
    
    // StatusCode 400 - FluentValidation
    private static void HandleValidationException(ExceptionContext context)
    {
        var exception = (ValidationException)context.Exception;

        var details = new ExceptionDetails
        (
            Reference: "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title: "One or more validation failures have occurred.",
            StatusCode: StatusCodes.Status400BadRequest,
            Message: exception.Errors
        );

        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }
    
    // StatusCode 400 - BadRequest
    private static void HandleBadRequestException(ExceptionContext context)
    {
        var exception = (BadRequestException)context.Exception;

        var details = new ExceptionDetails
        (
            Reference: "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            Title: "One or more validation failures have occurred.",
            StatusCode: StatusCodes.Status400BadRequest,
            Message: exception.Message
        );

        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }

    // StatusCode 404 - NotFound
    private static void HandleNotFoundException(ExceptionContext context)
    {
        var exception = (NotFoundException)context.Exception;

        var details = new ExceptionDetails
        (
            Reference: "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            Title: "The specified resource was not found.",
            StatusCode: StatusCodes.Status404NotFound,
            Message: exception.Message
        );

        context.Result = new NotFoundObjectResult(details);
        context.ExceptionHandled = true;
    }
}
