using System;
using System.Collections.Generic;

namespace RestaurantManagement.Models
{
    public partial class MenuSub
    {
        public int MenuSubId { get; set; }
        public string MenuGroupId { get; set; } = null!;
        public int MenuSubSort { get; set; }
        public string MenuSubName { get; set; } = null!;
        public string MenuSubUrl { get; set; } = null!;
        public int MenuSubPublish { get; set; }
        public DateTime CreateTime { get; set; }
        public int Creator { get; set; }
        public DateTime? EditTime { get; set; }
        public int? Editor { get; set; }
        public string? Ip { get; set; }
    }
}
