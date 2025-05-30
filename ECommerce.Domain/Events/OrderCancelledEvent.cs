namespace ECommerce.Domain.Events
{
    public record OrderCancelledEvent(Guid OrderId) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
