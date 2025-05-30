using ECommerce.Application.DTOs;
using MediatR;

namespace ECommerce.Application.Commands
{
    public class PlaceOrderCommand : IRequest<OrderDto>
    {
        public Guid CustomerId { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
