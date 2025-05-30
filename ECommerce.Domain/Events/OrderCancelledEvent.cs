namespace ECommerce.Domain.Events
{
    public record OrderCancelledEvent(Guid OrderId, Guid CustomerId) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
