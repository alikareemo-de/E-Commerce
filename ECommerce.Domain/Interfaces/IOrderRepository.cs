using ECommerce.Domain.Aggregates;

namespace ECommerce.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(Guid orderId);
        Task AddAsync(Order order);
        Task RemoveAsync(Order order);
        Task SaveChangesAsync();
        Task UpdateAsync(Order order);
    }
}
