//using ECommerce.Application.DTOs;
//using ECommerce.Domain.Interfaces;
//using MediatR;

//namespace ECommerce.Application.Commands
//{
//    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
//    {
//        private readonly IOrderRepository _orderRepository;

//        public GetOrderByIdHandler(IOrderRepository orderRepository)
//        {
//            _orderRepository = orderRepository;
//        }

//        public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
//        {
//            var order = await _orderRepository.GetByIdAsync(request.OrderId)
//                        ?? throw new Exception("Order not found");

//            return new OrderDto
//            {
//                OrderId = order.OrderId,
//                Status = order.OrderStatus.ToString(),
//                TotalPrice = order.TotalPrice.ToString(),
//                Items = order.OrderItems.Select(i => new OrderItemDto
//                {
//                    ProductRef = i.ProductRef,
//                    Quantity = i.Quantity,
//                    Price = i.Price.Amount
//                }).ToList()
//            };
//        }
//    }
//}
