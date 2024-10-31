using Application.Abstraction;
using Application.Features.Users.Commands.LoginUser;
using Application.Features.Users.Commands.RegisterUser;
using Microsoft.Extensions.Logging;

public class LoggingBehavior<TRequest, TResponse>(
    ILogger<LoggingBehavior<TRequest, TResponse>> logger,
    IUserService userService)
    : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string? userId = null;

        // Token gerektirmeyen işlemler için `GetUserId` çağrısını atlayın
        if (request is not LoginUserCommand && request is not RegisterUserCommand)
        {
            userId = userService.GetUserId();
        }

        logger.LogInformation("User {UserId} is executing {RequestName} with data: {RequestData}", userId ?? "Anonymous", typeof(TRequest).Name, request);

        var response = await next();

        logger.LogInformation("User {UserId} completed {RequestName} with result: {ResponseData}", userId ?? "Anonymous", typeof(TRequest).Name, response);

        return response;
    }
}
