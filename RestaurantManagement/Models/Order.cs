using System;
using System.Collections.Generic;

namespace RestaurantManagement.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int MemberId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
