using System;
using System.Collections.Generic;

namespace RestaurantManagement.Models
{
    public partial class Permission
    {
        public int AdminId { get; set; }
        public string AdminName { get; set; } = null!;
        public string GroupName { get; set; } = null!;
        public string Role { get; set; } = null!;
        public int MenuSubId { get; set; }
    }
}
