using System.ComponentModel.DataAnnotations;
using System.Data;

namespace RestaurantManagement.BackEnd.ViewModel.Banner
{
    public class BannerIndexViewModel
    {
        [Display(Name = "編號")]
        public long BannerId { get; set; }

        [Display(Name = "標題")]
        public string BannerTitle { get; set; }

        [Display(Name = "狀態")]
        public int BannerPublish { get; set; }

        [Display(Name = "新增時間")]
        public DateTime CreateTime { get; set; }

        [Display(Name = "更新時間")]
        public DateTime? EditTime { get; set; }
    }

}
