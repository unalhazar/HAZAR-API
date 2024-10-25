using Application.Abstraction;
using Microsoft.Extensions.Logging;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    private readonly IUserService _userService;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var userId = _userService.GetUserId();
        _logger.LogInformation("User {UserId} is executing {RequestName} with data: {RequestData}", userId, typeof(TRequest).Name, request);

        var response = await next();

        _logger.LogInformation("User {UserId} completed {RequestName} with result: {ResponseData}", userId, typeof(TRequest).Name, response);

        return response;
    }
}
