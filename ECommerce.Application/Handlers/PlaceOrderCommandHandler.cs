using AutoMapper;
using ECommerce.Application.Commands;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.Services;
using ECommerce.Domain.ValueObjects;
using MediatR;

namespace ECommerce.Application.Handlers
{
    public class PlaceOrderCommandHandler : IRequestHandler<PlaceOrderCommand, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly OrderService _orderService;
        private readonly IMapper _mapper;

        public PlaceOrderCommandHandler(
            IOrderRepository orderRepository,
            ICustomerRepository customerRepository,
            OrderService orderService,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _orderService = orderService;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(PlaceOrderCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
            if (customer == null)
                throw new Exception("Customer not found");

            var items = request.Items.Select(dto =>
                new OrderItem(dto.ProductRef, dto.Quantity, new Money(dto.Amount, dto.Currency))
                ).ToList();

            var order = _orderService.PlaceOrder(request.CustomerId, items);
            await _orderRepository.AddAsync(order);

            return _mapper.Map<OrderDto>(order);
        }
    }
}
