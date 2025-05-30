namespace ECommerce.Domain.Events
{
    public record OrderShippedEvent(Guid OrderId, Guid CustomerId) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
