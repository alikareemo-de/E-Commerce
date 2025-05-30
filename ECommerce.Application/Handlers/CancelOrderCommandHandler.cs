using ECommerce.Application.Commands;
using ECommerce.Domain.Exceptions;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Services;
using MediatR;

namespace ECommerce.Application.Handlers
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly OrderService _orderService;

        public CancelOrderCommandHandler(IOrderRepository orderRepository, OrderService orderService)
        {
            _orderRepository = orderRepository;
            _orderService = orderService;
        }

        public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            if (order == null)
                throw new NotFoundException("Order not found");

            _orderService.CancelOrder(order, request.CancellationReason);

            await _orderRepository.UpdateAsync(order);

            return Unit.Value;
        }
    }
}
