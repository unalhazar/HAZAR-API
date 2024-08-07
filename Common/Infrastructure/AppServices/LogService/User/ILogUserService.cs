using Domain;

namespace Infrastructure.AppServices.LogService.User
{
    public interface ILogUserService
    {
        void LogUser(string message, string operation, string userId = null, LogUserLevel? logLevel = null);
    }
}

