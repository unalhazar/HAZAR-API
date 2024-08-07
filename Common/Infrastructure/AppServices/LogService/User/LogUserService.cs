using Domain;

namespace Infrastructure.AppServices.LogService.User
{
    public class LogUserService : ILogUserService
    {
        private readonly ILogUserRepository loggingRepository;

        public LogUserService(HazarDbContext dbContext, ILogUserRepository loggingRepository)
        {
            this.loggingRepository = loggingRepository;
        }

        public async void LogUser(string message, string operation, string userId = null, LogUserLevel? logLevel = null)
        {
            var logEntry = new LogUser
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
