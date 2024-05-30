using System;
using System.Collections.Generic;

namespace RestaurantManagement.Models
{
    public partial class AdminRole
    {
        public int RoleId { get; set; }
        public int GroupId { get; set; }
        public int MenuSubId { get; set; }
        public string Role { get; set; } = null!;
        public DateTime CreateTime { get; set; }
        public int Creator { get; set; }
        public string? Ip { get; set; }
    }
}
