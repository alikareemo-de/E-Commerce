//using ECommerce.Domain.Entities;
//using ECommerce.Domain.Interfaces;
//using ECommerce.Domain.Services;
//using ECommerce.Domain.ValueObjects;
//using MediatR;

//namespace ECommerce.Application.Commands;

//public class PlaceOrderHandler : IRequestHandler<PlaceOrderCommand, Guid>
//{
//    private readonly ICustomerRepository _customerRepository;
//    private readonly IOrderRepository _orderRepository;
//    private readonly OrderService _orderService;

//    public PlaceOrderHandler(
//        ICustomerRepository customerRepository,
//        IOrderRepository orderRepository,
//        OrderService orderService)
//    {
//        _customerRepository = customerRepository;
//        _orderRepository = orderRepository;
//        _orderService = orderService;
//    }

//    public async Task<Guid> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
//    {
//        var customer = await _customerRepository.GetByIdAsync(request.CustomerId)
//                      ?? throw new Exception("Customer not found");

//        var orderItems = request.Items.Select(i =>
//            new OrderItem(i.ProductRef, i.Quantity, new Money(i.Price))
//        ).ToList();

//        var order = _orderService.PlaceOrder(customer.CustomerId, orderItems);

//        await _orderRepository.AddAsync(order);
//        await _orderRepository.SaveChangesAsync();

//        return order.OrderId;
//    }
//}
