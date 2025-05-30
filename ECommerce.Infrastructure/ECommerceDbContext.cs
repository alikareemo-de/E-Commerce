using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;
using ECommerce.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ECommerce.Infrastructure
{
    public class ECommerceDbContext : DbContext
    {
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();

        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Value Converter for Email
            var emailConverter = new ValueConverter<Email, string>(
                email => email.Value,
                value => new Email(value));

            // 2. Customer Configuration
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(c => c.CustomerId);

                entity.Property(c => c.Email)
                    .HasConversion(emailConverter)
                    .HasMaxLength(255);
            });


            // 3. Order Configuration
            modelBuilder.Entity<Order>(order =>
            {
                order.HasKey(o => o.OrderId);

                // 3.1 Configure TotalPrice as owned type
                order.OwnsOne(o => o.TotalPrice, money =>
                {
                    money.Property(m => m.Amount).HasColumnName("TotalAmount");
                    money.Property(m => m.Currency).HasColumnName("Currency").HasMaxLength(3);
                });

                // 3.2 Configure OrderItems as owned collection
                order.OwnsMany(o => o.OrderItems, item =>
                {
                    item.WithOwner().HasForeignKey("OrderId");

                    // Configure Price in OrderItem
                    item.OwnsOne(i => i.Price, price =>
                    {
                        price.Property(p => p.Amount).HasColumnName("Amount");
                        price.Property(p => p.Currency).HasColumnName("Currency").HasMaxLength(3);
                    });

                    // Optional: Set table name explicitly
                    item.ToTable("OrderItems");
                });
                modelBuilder.Entity<Order>()
                    .Ignore(o => o.TotalPrice);

                // 3.3 Configure State property (if needed)
                order.Property(o => o.OrderStatus)
                    .HasConversion<string>()
                    .HasMaxLength(20);
            });
        }
    }
}