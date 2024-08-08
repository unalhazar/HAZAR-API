namespace Application.Abstraction
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string user, string message);
    }
}
