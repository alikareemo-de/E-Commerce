namespace ECommerce.Domain.Events
{
    public record OrderDeliveredEvent(Guid OrderId, Guid CustomerId) : IDomainEvent
    {
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
    }
}
