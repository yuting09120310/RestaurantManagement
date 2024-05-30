using System;
using System.Collections.Generic;

namespace RestaurantManagement.Models
{
    public partial class AdminGroup
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; } = null!;
        public string GroupInfo { get; set; } = null!;
        public int GroupPublish { get; set; }
        public DateTime CreateTime { get; set; }
        public int Creator { get; set; }
        public DateTime? EditTime { get; set; }
        public int? Editor { get; set; }
        public string? Ip { get; set; }
    }
}
