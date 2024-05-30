using System;
using System.Collections.Generic;

namespace RestaurantManagement.Models
{
    public partial class MenuGroup
    {
        public int MenuGroupId { get; set; }
        public int MenuGroupSort { get; set; }
        public string MenuGroupName { get; set; } = null!;
        public string MenuGroupIcon { get; set; } = null!;
        public string MenuGroupInfo { get; set; } = null!;
        public string MenuGroupUrl { get; set; } = null!;
        public int MenuGroupPublish { get; set; }
        public DateTime CreateTime { get; set; }
        public int Creator { get; set; }
        public DateTime? EditTime { get; set; }
        public int? Editor { get; set; }
        public string? Ip { get; set; }
    }
}
