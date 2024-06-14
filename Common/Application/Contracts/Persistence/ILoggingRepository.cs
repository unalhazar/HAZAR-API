using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface ILoggingRepository : IAsyncRepository<Log>
    {
    }
}
