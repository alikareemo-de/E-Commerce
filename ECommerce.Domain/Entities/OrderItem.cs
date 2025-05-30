using ECommerce.Domain.ValueObjects;

namespace ECommerce.Domain.Entities
{
    public class OrderItem
    {

        public Guid Id { get; private set; } = Guid.NewGuid();
        public string ProductRef { get; private set; }
        public int Quantity { get; private set; }
        public Money Price { get; private set; }

        public string Currency { get; set; } = "USD";

        public OrderItem(string productRef, int quantity, Money price)
        {
            Id = Guid.NewGuid();
            ProductRef = productRef;
            Quantity = quantity;
            Price = price ?? throw new ArgumentNullException(nameof(price));
        }

        private OrderItem() { }
    }
}
