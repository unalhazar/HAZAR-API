using Serilog;

namespace Infrastructure.AppServices.LogService.LoggingService
{
    public interface ILoggingService
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message, Exception ex);
    }

    public class LoggingService : ILoggingService
    {
        public void LogInformation(string message)
        {
            Log.Information(message);
        }

        public void LogWarning(string message)
        {
            Log.Warning(message);
        }

        public void LogError(string message, Exception ex)
        {
            Log.Error(ex, message);
        }
    }
}
