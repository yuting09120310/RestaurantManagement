namespace RestaurantManagement.API.Dtos
{
    public class CartDto
    {

        public class CartItemDto
        {
            public int CartId { get; set; }
            public int MemberId { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
            public DateTime CreatedDate { get; set; }
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public string ProductImg1 { get; set; }
        }


        public class CartCreateDto
        {
            public int MemberId { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }


        public class CartUpdateDto
        {
            public int CartId { get; set; }
            public int MemberId { get; set; }
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }


        public class CartDeleteDto
        {
            public int CartId { get; set; }
        }


    }
}
