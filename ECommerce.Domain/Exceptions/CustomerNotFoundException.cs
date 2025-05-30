namespace ECommerce.Domain.Exceptions
{
    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException(Guid customerId)
            : base($"Customer with ID {customerId} not found") { }
    }
}
