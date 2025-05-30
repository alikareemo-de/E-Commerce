using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(Guid id);
        Task AddAsync(Customer customer);

        Task<bool> ExistsAsync(Guid id);
        Task SaveChangesAsync();
    }
}
