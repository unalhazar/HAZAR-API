using Application.Abstraction;
using Microsoft.Extensions.Logging;

namespace Infrastructure.AppServices.LogService.GlobalException
{
    public class GlobalLoggingService(IGlobalLoggingRepository loggingRepository, ILogger<GlobalLoggingService> logger)
        : IGlobalLoggingService
    {
        public async Task LogAsync(string message, string operation, string userId = null, LogLevel logLevel = LogLevel.Error)
        {
            var logEntry = new GlobalLog
            {
                Message = message,
                Operation = operation,
                UserId = userId,
                CreatedDate = DateTime.Now,
                LogLevel = logLevel
            };

            // Veritabanına log yazma
            await loggingRepository.AddAsync(logEntry);

            // Konsola log yazma
            switch (logLevel)
            {
                case LogLevel.Trace:
                    logger.LogTrace(message);
                    break;
                case LogLevel.Debug:
                    logger.LogDebug(message);
                    break;
                case LogLevel.Information:
                    logger.LogInformation(message);
                    break;
                case LogLevel.Warning:
                    logger.LogWarning(message);
                    break;
                case LogLevel.Error:
                    logger.LogError(message);
                    break;
                case LogLevel.Critical:
                    logger.LogCritical(message);
                    break;
                default:
                    logger.LogError(message);
                    break;
            }
        }
    }
}
