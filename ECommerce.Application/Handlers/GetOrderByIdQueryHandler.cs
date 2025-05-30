using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Application.Queries;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Handlers
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            if (order == null)
                throw new Exception("Order not found");

            return _mapper.Map<OrderDto>(order);
        }
    }
}
