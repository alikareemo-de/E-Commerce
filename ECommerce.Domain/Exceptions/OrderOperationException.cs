namespace ECommerce.Domain.Exceptions
{
    public class OrderOperationException : Exception
    {
        public OrderOperationException(string message, Exception inner)
            : base(message, inner) { }
    }
}
