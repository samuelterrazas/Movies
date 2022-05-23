namespace Movies.Common.Behaviours;

public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    
    public UnhandledExceptionBehaviour(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<TRequest>();
    }
    
    
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;
            
            _logger.LogError(ex, "Unhandled Exception for Request \"{Name}\" \"{@Request}\"", requestName, request);
            throw;
        }
    }
}