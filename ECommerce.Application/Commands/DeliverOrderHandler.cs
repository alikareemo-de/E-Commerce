//using ECommerce.Domain.Exceptions;
//using ECommerce.Domain.Interfaces;
//using ECommerce.Domain.Services;
//using MediatR;

//namespace ECommerce.Application.Commands
//{
//    public class DeliverOrderHandler : IRequestHandler<DeliverOrderCommand>
//    {
//        private readonly IOrderRepository _orderRepository;
//        private readonly OrderService _orderService;

//        public DeliverOrderHandler(IOrderRepository orderRepository, OrderService orderService)
//        {
//            _orderRepository = orderRepository;
//            _orderService = orderService;
//        }

//        public async Task<Unit> Handle(DeliverOrderCommand request, CancellationToken cancellationToken)
//        {
//            var order = await _orderRepository.GetByIdAsync(request.OrderId)
//                        ?? throw new DomainException("Order not found");

//            try
//            {
//                _orderService.DeliverOrder(order);
//                await _orderRepository.SaveChangesAsync();
//            }
//            catch (DomainException ex)
//            {
//                throw new DomainException($"Cannot deliver order: {ex.Message}");
//            }

//            return Unit.Value;
//        }
//    }
//}
