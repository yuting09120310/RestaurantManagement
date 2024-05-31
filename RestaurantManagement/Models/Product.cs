using Microsoft.AspNetCore.Components.Web.Virtualization;
using System;
using System.Collections.Generic;

namespace RestaurantManagement.Models
{
    public partial class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }
        public string ProductImg1 { get; set; } = null!;
        public decimal Price { get; set; }
        public virtual int ProductClassId { get; set; }
    }
}
