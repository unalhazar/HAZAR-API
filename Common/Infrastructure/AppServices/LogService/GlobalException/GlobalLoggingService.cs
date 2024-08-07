using Microsoft.Extensions.Logging;

namespace Infrastructure.AppServices.LogService.GlobalException
{
    public class GlobalLoggingService : IGlobalLoggingService
    {
        private readonly IGlobalLoggingRepository _loggingRepository;
        private readonly ILogger<GlobalLoggingService> _logger;

        public GlobalLoggingService(IGlobalLoggingRepository loggingRepository, ILogger<GlobalLoggingService> logger)
        {
            _loggingRepository = loggingRepository;
            _logger = logger;
        }

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
            await _loggingRepository.AddAsync(logEntry);

            // Konsola log yazma
            switch (logLevel)
            {
                case LogLevel.Trace:
                    _logger.LogTrace(message);
                    break;
                case LogLevel.Debug:
                    _logger.LogDebug(message);
                    break;
                case LogLevel.Information:
                    _logger.LogInformation(message);
                    break;
                case LogLevel.Warning:
                    _logger.LogWarning(message);
                    break;
                case LogLevel.Error:
                    _logger.LogError(message);
                    break;
                case LogLevel.Critical:
                    _logger.LogCritical(message);
                    break;
                default:
                    _logger.LogError(message);
                    break;
            }
        }
    }
}
