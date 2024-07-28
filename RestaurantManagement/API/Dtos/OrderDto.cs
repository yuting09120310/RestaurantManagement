namespace RestaurantManagement.API.Dtos
{
    public class OrderCreateDto
    {
        public int MemberId { get; set; }
    }

    public class OrderDetailDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class OrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int MemberId { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderDetailDto> OrderDetails { get; set; }
    }
}
