using ECommerce.Application.Commands;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Handlers
{
    public class ShipOrderCommandHandler : IRequestHandler<ShipOrderCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;

        public ShipOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(ShipOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            if (order == null)
                throw new Exception("Order not found");

            order.Ship();
            await _orderRepository.UpdateAsync(order);

            return Unit.Value;
        }
    }
}
