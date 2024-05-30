using System;
using System.Collections.Generic;

namespace RestaurantManagement.Models
{
    public partial class Contact
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; } = null!;
        public string ContactPhone { get; set; } = null!;
        public string ContactMail { get; set; } = null!;
        public string ContactTxt { get; set; } = null!;
        public string? ContactReTxt { get; set; }
        public DateTime CreateTime { get; set; }
        public string Ip { get; set; } = null!;
    }
}
