using MediatR;

namespace ECommerce.Application.Commands
{
    public class ShipOrderCommand : IRequest<Unit>
    {
        public Guid OrderId { get; set; }
    }
}
