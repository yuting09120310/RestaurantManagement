using System;
using System.Collections.Generic;

namespace RestaurantManagement.Models
{
    public partial class View1
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }
        public string ProductImg1 { get; set; } = null!;
        public decimal Price { get; set; }
        public int ProductClassId { get; set; }
        public int Expr1 { get; set; }
        public string Expr2 { get; set; } = null!;
        public string ProductClassName { get; set; } = null!;
    }
}
