using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Exceptions;

namespace ECommerce.Domain.Services
{
    public class OrderService
    {
        public Order PlaceOrder(Guid customerId, List<OrderItem> items)
        {
            return new Order(Guid.NewGuid(), customerId, items);
        }


        public void CancelOrder(Order order, string cancellationReason)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            try
            {
                order.Cancel(cancellationReason);
            }
            catch (DomainException ex)
            {
                throw new OrderOperationException("Cancellation failed", ex);
            }
        }

    }
}
