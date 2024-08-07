using Domain;

namespace Application.Abstraction
{
    public interface ILogUserService
    {
        void LogUser(string message, string operation, string userId = null, LogUserLevel? logLevel = null);
    }
}
