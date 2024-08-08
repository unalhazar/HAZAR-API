using Application.Abstraction;
using Domain.Events;

namespace Application.EventHandlers
{
    public class ProductCreatedEventHandler : INotificationHandler<ProductCreatedEvent>
    {
        private readonly INotificationService _notificationService;

        public ProductCreatedEventHandler(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            await _notificationService.SendNotificationAsync("admin", $"New product created: {notification.Name}");
        }
    }
}
