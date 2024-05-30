using System;
using System.Collections.Generic;

namespace RestaurantManagement.Models
{
    public partial class Banner
    {
        public int BannerId { get; set; }
        public int? BannerSort { get; set; }
        public string BannerTitle { get; set; } = null!;
        public string? BannerDescription { get; set; }
        public string BannerContxt { get; set; } = null!;
        public string BannerImg1 { get; set; } = null!;
        public string? BannerImgUrl { get; set; }
        public string? BannerImgAlt { get; set; }
        public int BannerPublish { get; set; }
        public DateTime BannerPutTime { get; set; }
        public DateTime BannerOffTime { get; set; }
        public DateTime CreateTime { get; set; }
        public int Creator { get; set; }
        public DateTime? EditTime { get; set; }
        public int? Editor { get; set; }
        public string? Ip { get; set; }
    }
}
