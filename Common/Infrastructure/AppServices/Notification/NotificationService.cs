using Application.Abstraction;
using Infrastructure.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.AppServices.Notification
{
    public class NotificationService(IHubContext<NotificationHub> hubContext) : INotificationService
    {
        public async Task SendNotificationAsync(string user, string message)
        {
            await hubContext.Clients.All.SendAsync("ReceiveNotification", user, message);
        }
    }
}
