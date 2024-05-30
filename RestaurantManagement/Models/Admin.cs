using System;
using System.Collections.Generic;

namespace RestaurantManagement.Models
{
    public partial class Admin
    {
        public int AdminId { get; set; }
        public int GroupId { get; set; }
        public string AdminAcc { get; set; } = null!;
        public string AdminPwd { get; set; } = null!;
        public string AdminName { get; set; } = null!;
        public int AdminPublish { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime CreateTime { get; set; }
        public int Creator { get; set; }
        public DateTime? EditTime { get; set; }
        public int? Editor { get; set; }
        public string? Ip { get; set; }
    }
}
