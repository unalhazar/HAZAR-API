using Microsoft.Extensions.Logging;

namespace Infrastructure.AppServices.LogService.GlobalException
{
    public interface IGlobalLoggingService
    {
        Task LogAsync(string message, string operation, string userId = null, LogLevel logLevel = LogLevel.Error);
    }
}
