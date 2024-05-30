using System;
using System.Collections.Generic;

namespace RestaurantManagement.Models
{
    public partial class ProductClass
    {
        public ProductClass()
        {
            Products = new HashSet<Product>();
        }

        public int ProductClassId { get; set; }
        public string ProductClassName { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
