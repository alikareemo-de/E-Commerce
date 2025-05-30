namespace ECommerce.Application.DTOs
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public string OrderStatus { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public string Currency { get; set; } = string.Empty;
        public List<OrderItemDto> Items { get; set; } = new();
    }
}
