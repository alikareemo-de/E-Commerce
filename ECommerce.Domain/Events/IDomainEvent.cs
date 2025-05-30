using MediatR;

namespace ECommerce.Domain.Events
{
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}
