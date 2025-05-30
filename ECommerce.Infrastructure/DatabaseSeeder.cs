using ECommerce.Domain.Entities;
using ECommerce.Domain.ValueObjects;

namespace ECommerce.Infrastructure
{
    public class DatabaseSeeder
    {
        public static void Seed(ECommerceDbContext dbContext)
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.AddRange(
                    new Customer(Guid.Parse("11111111-1111-1111-1111-111111111111"), "John Doe", new Email("email@example.com")),
                    new Customer(Guid.Parse("22222222-2222-2222-2222-222222222222"), "Jane Smith", new Email("jane@example.com"))
                );
                dbContext.SaveChanges();
            }
        }
    }
}
