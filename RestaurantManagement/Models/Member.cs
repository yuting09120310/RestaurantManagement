using System;
using System.Collections.Generic;

namespace RestaurantManagement.Models
{
    public partial class Member
    {
        public int MemberId { get; set; }
        public string MemberAccount { get; set; } = null!;
        public string MemberPassword { get; set; } = null!;
        public string MemberName { get; set; } = null!;
        public string MemberPhone { get; set; } = null!;
        public string MemberEmail { get; set; } = null!;
        public int MemberPublish { get; set; }
        public DateTime? LastLogin { get; set; }
        public string? Ip { get; set; }
    }
}
