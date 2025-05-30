using ECommerce.Application.Commands;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Handlers
{
    public class ConfirmOrderCommandHandler : IRequestHandler<ConfirmOrderCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;

        public ConfirmOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Unit> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            if (order == null)
                throw new Exception("Order not found");

            order.Confirm();
            await _orderRepository.UpdateAsync(order);

            return Unit.Value;
        }
    }
}
