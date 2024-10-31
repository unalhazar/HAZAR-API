using Application.Abstraction;
using Application.Features.Users.Commands.LoginUser;
using Application.Features.Users.Commands.RegisterUser;
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
        string userId = null;

        // Token gerektirmeyen işlemler için `GetUserId` çağrısını atlayın
        if (request is not LoginUserCommand && request is not RegisterUserCommand)
        {
            userId = _userService.GetUserId();
        }

        _logger.LogInformation("User {UserId} is executing {RequestName} with data: {RequestData}", userId ?? "Anonymous", typeof(TRequest).Name, request);

        var response = await next();

        _logger.LogInformation("User {UserId} completed {RequestName} with result: {ResponseData}", userId ?? "Anonymous", typeof(TRequest).Name, response);

        return response;
    }
}
