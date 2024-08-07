using Microsoft.Extensions.Logging;

namespace Application.Abstraction
{
    public interface IGlobalLoggingService
    {
        Task LogAsync(string message, string operation, string userId = null, LogLevel logLevel = LogLevel.Error);
    }
}
