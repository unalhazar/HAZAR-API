using Domain;

namespace Infrastructure.AppServices.LogService
{
    public class LoggingService : ILoggingService
    {
        private readonly ILoggingRepository loggingRepository;

        public LoggingService(HazarDbContext dbContext, ILoggingRepository loggingRepository)
        {
            this.loggingRepository = loggingRepository;
        }

        public async void Log(string message, string operation, string userId = null, LogLevel? logLevel = null)
        {
            var logEntry = new Log
            {
                Message = message,
                Operation = operation,
                UserId = userId,
                CreatedDate = DateTime.Now,
                LogLevel = logLevel
            };

            await loggingRepository.AddAsync(logEntry);
        }
    }
}
