using Domain;

namespace Infrastructure.AppServices.LogService
{
    public interface ILoggingService
    {
        void Log(string message, string operation, string userId = null, LogLevel? logLevel = null);
    }
}

