using MediatR;

namespace ECommerce.Application.Commands
{
    public class ConfirmOrderCommand : IRequest<Unit>
    {
        public Guid OrderId { get; set; }
    }
}
