using MediatR;

namespace ECommerce.Application.Commands
{
    public class CancelOrderCommand : IRequest<Unit>
    {
        public Guid OrderId { get; set; }
        public string CancellationReason { get; set; } = string.Empty;
    }
}