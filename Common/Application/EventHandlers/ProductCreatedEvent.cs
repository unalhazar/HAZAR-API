namespace Application.EventHandlers
{
    public class ProductCreatedEvent : INotification
    {
        public long ProductId { get; set; }
        public string Name { get; set; }

        public ProductCreatedEvent(long productId, string name)
        {
            ProductId = productId;
            Name = name;
        }
    }
}
