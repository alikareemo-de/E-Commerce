namespace ECommerce.Application.DTOs
{
    public class OrderItemDto
    {
        public string ProductRef { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;
    }
}
