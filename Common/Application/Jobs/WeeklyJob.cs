using Microsoft.Extensions.Logging;

namespace Application.Jobs
{
    public class WeeklyJob
    {
        private readonly ILogger<WeeklyJob> _logger;

        public WeeklyJob(ILogger<WeeklyJob> logger)
        {
            _logger = logger;
        }

        public void Execute()
        {
            _logger.LogInformation("1 dakika oldu.");
        }
    }
}
