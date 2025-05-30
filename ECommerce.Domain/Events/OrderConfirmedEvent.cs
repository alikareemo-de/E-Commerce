namespace ECommerce.Domain.Events
{
    public record OrderConfirmedEvent(Guid OrderId) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
