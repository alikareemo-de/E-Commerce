//using ECommerce.Application.DTOs;
//using ECommerce.Domain.Aggregates;
//using ECommerce.Domain.Entities;
//using ECommerce.Domain.Interfaces;
//using ECommerce.Domain.ValueObjects;

//namespace ECommerce.Application.Queries
//{
//    public class OrderAppService : IOrderAppService
//    {
//        private readonly IOrderRepository _orderRepository;
//        private readonly ICustomerRepository _customerRepository;

//        public OrderAppService(IOrderRepository orderRepository, ICustomerRepository customerRepository)
//        {
//            _orderRepository = orderRepository;
//            _customerRepository = customerRepository;
//        }


//        public async Task<Guid> PlaceOrderAsync(Guid customerId, List<OrderItemDto> items)
//        {
//            var customer = await _customerRepository.GetByIdAsync(customerId);
//            if (customer == null)
//                throw new Exception("Customer not found.");

//            var order = new Order(Guid.NewGuid(), customer.CustomerId);

//            foreach (var item in items)
//            {
//                var price = new Money(item.Price);
//                var orderItem = new OrderItem(item.ProductRef, item.Quantity, price);
//                order.AddItem(orderItem);
//            }

//            await _orderRepository.AddAsync(order);
//            await _orderRepository.SaveChangesAsync();

//            return order.OrderId;
//        }

//        public async Task<OrderDto?> GetOrderByIdAsync(Guid orderId)
//        {
//            var order = await _orderRepository.GetByIdAsync(orderId);
//            if (order == null)
//                return null;

//            return new OrderDto
//            {
//                OrderId = order.OrderId,
//                Status = order.OrderStatus.ToString(),
//                TotalPrice = order.TotalPrice.ToString(),
//                Items = order.OrderItems.Select(oi => new OrderItemDto
//                {
//                    ProductRef = oi.ProductRef,
//                    Quantity = oi.Quantity,
//                    Price = oi.Price.Amount
//                }).ToList()
//            };
//        }

//        public async Task<bool> CancelOrderAsync(Guid orderId)
//        {
//            var order = await _orderRepository.GetByIdAsync(orderId);
//            if (order == null)
//                return false; // ✅ إرجاع false بدل رمي استثناء

//            order.Cancel();
//            await _orderRepository.SaveChangesAsync(); // ✅ حفظ التغييرات
//            return true;
//        }
//    }
//}
