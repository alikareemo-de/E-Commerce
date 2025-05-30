namespace ECommerce.Domain.Events
{
    public record OrderPlacedEvent(Guid OrderId) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
