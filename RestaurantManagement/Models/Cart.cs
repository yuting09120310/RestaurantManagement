using System;
using System.Collections.Generic;

namespace RestaurantManagement.Models
{
    public partial class Cart
    {
        public int CartId { get; set; }
        public int MemberId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
