//using ECommerce.Domain.Interfaces;
//using ECommerce.Domain.Services;
//using MediatR;

//namespace ECommerce.Application.Commands;

//public class CancelOrderHandler : MediatR.IRequestHandler<ECommerce.Application.Commands.CancelOrderCommand>
//{
//    private readonly IOrderRepository _orderRepository;
//    private readonly OrderService _orderService;

//    public CancelOrderHandler(IOrderRepository orderRepository, OrderService orderService)
//    {
//        _orderRepository = orderRepository;
//        _orderService = orderService;
//    }

//    public async Task<Unit> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
//    {
//        var order = await _orderRepository.GetByIdAsync(request.OrderId)
//                    ?? throw new Exception("Order not found");

//        _orderService.CancelOrder(order);
//        await _orderRepository.SaveChangesAsync();

//        return Unit.Value;
//    }


//}
