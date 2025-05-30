using ECommerce.Domain.ValueObjects;

namespace ECommerce.Domain.Entities
{
    public class Customer
    {
        public Guid CustomerId { get; private set; }
        public string Name { get; private set; }
        public Email Email { get; private set; }

        public Customer(Guid customerId, string name, Email email)
        {
            CustomerId = customerId;
            Name = name;
            Email = email;
        }

        private Customer() { }
    }

}
