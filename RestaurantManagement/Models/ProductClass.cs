using System;
using System.Collections.Generic;

namespace RestaurantManagement.Models
{
    public partial class ProductClass
    {
        public int ProductClassId { get; set; }
        public string ProductClassName { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
