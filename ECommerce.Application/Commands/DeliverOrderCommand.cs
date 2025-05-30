using MediatR;

namespace ECommerce.Application.Commands
{
    public class DeliverOrderCommand : IRequest<Unit>
    {
        public Guid OrderId { get; set; }
    }
}
