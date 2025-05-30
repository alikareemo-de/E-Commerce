using ECommerce.Application.DTOs;

namespace ECommerce.Application.Queries
{
    public interface IOrderAppService
    {
        Task<Guid> PlaceOrderAsync(Guid customerId, List<OrderItemDto> items);
        Task<OrderDto?> GetOrderByIdAsync(Guid id);
        Task<bool> CancelOrderAsync(Guid orderId);
    }
}
