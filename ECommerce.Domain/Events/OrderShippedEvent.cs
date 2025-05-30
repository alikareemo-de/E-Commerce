namespace ECommerce.Domain.Events
{
    public record OrderShippedEvent(Guid OrderId) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
