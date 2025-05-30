using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;
using ECommerce.Domain.Events;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.ValueObjects;
using Stateless;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Domain.Aggregates
{
    public class Order
    {
        public Guid OrderId { get; private set; }
        private List<OrderItem> _orderItems = new();
        public Guid CustomerId { get; private set; }
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();


        public string? CancellationReason { get; private set; }
        public List<IDomainEvent> DomainEvents { get; } = new();
        public OrderStatus OrderStatus { get; private set; }

        [NotMapped]
        public Money TotalPrice => CalculateTotalPrice();

        private StateMachine<OrderStatus, OrderTrigger> _stateMachine;


        private Order()
        {
        }
        public Order(Guid orderId, Guid customerId, List<OrderItem> items)
        {
            OrderId = orderId;
            CustomerId = customerId;
            OrderStatus = OrderStatus.Pending;

            if (items == null || !items.Any())
                throw new DomainException("Order must contain at least one item.");

            _orderItems = items;
            ConfigureStateMachine();

            DomainEvents.Add(new OrderPlacedEvent(OrderId));
        }
        public void ConfigureStateMachine()
        {
            _stateMachine = new StateMachine<OrderStatus, OrderTrigger>(() => OrderStatus, s => OrderStatus = s);

            _stateMachine.Configure(OrderStatus.Pending)
                .Permit(OrderTrigger.Confirm, OrderStatus.Confirmed)
                .Permit(OrderTrigger.Cancel, OrderStatus.Cancelled);

            _stateMachine.Configure(OrderStatus.Confirmed)
                .Permit(OrderTrigger.Ship, OrderStatus.Shipped)
                .Permit(OrderTrigger.Cancel, OrderStatus.Cancelled);

            _stateMachine.Configure(OrderStatus.Shipped)
                .Permit(OrderTrigger.Deliver, OrderStatus.Delivered);

            _stateMachine.Configure(OrderStatus.Delivered)
                .Ignore(OrderTrigger.Confirm)
                .Ignore(OrderTrigger.Ship)
                .Ignore(OrderTrigger.Cancel)
                .Ignore(OrderTrigger.Deliver);

            _stateMachine.Configure(OrderStatus.Cancelled)
                .Ignore(OrderTrigger.Confirm)
                .Ignore(OrderTrigger.Ship)
                .Ignore(OrderTrigger.Cancel)
                .Ignore(OrderTrigger.Deliver);
        }

        public void AddItem(OrderItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _orderItems.Add(item);
        }

        private Money CalculateTotalPrice()
        {
            decimal total = _orderItems.Sum(i => i.Price.Amount * i.Quantity);
            return new Money(total, _orderItems.FirstOrDefault()?.Price?.Currency ?? "USD");
        }

        private void FireTrigger(OrderTrigger trigger, IDomainEvent domainEvent)
        {
            if (!_stateMachine.CanFire(trigger))
                throw new DomainException($"Cannot {trigger} order when in {OrderStatus} status.");

            _stateMachine.Fire(trigger);
            DomainEvents.Add(domainEvent);
        }


        public void Confirm()
        {
            FireTrigger(OrderTrigger.Confirm, new OrderConfirmedEvent(OrderId));
        }

        public void Ship()
        {
            FireTrigger(OrderTrigger.Ship, new OrderConfirmedEvent(OrderId));

        }

        public void Deliver()
        {
            FireTrigger(OrderTrigger.Deliver, new OrderConfirmedEvent(OrderId));


        }

        public void Cancel(string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
                throw new DomainException("Cancellation reason is required.");

            CancellationReason = reason;
            FireTrigger(OrderTrigger.Cancel, new OrderCancelledEvent(OrderId));
        }
    }
}