//using ECommerce.Domain.Interfaces;
//using ECommerce.Domain.Services;
//using MediatR;

//namespace ECommerce.Application.Commands
//{
//    public class ConfirmOrderHandler : IRequestHandler<ConfirmOrderCommand>
//    {
//        private readonly IOrderRepository _orderRepository;
//        private readonly OrderService _orderService;

//        public ConfirmOrderHandler(IOrderRepository orderRepository, OrderService orderService)
//        {
//            _orderRepository = orderRepository;
//            _orderService = orderService;
//        }

//        public async Task<Unit> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
//        {
//            var order = await _orderRepository.GetByIdAsync(request.OrderId)
//                        ?? throw new Exception("Order not found");

//            try
//            {
//                _orderService.ConfirmOrder(order);
//                await _orderRepository.SaveChangesAsync();
//            }
//            catch (Exception ex)
//            {
//                throw new Exception($"Cannot confirm order: {ex.Message}");
//            }

//            return Unit.Value;
//        }
//    }
//}
