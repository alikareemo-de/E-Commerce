namespace ECommerce.Domain.Events
{
    public record OrderDeliveredEvent(Guid OrderId) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
