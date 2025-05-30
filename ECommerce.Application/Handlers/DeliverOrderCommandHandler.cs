using ECommerce.Application.Commands;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Handlers
{
    public class DeliverOrderCommandHandler : IRequestHandler<DeliverOrderCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;

        public DeliverOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(DeliverOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            if (order == null)
                throw new Exception("Order not found");

            order.Deliver();
            await _orderRepository.UpdateAsync(order);

            return Unit.Value;
        }
    }
}
