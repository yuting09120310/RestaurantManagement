using System.ComponentModel.DataAnnotations;
using System.Data;

namespace RestaurantManagement.BackEnd.ViewModel.News
{
    public class NewsIndexViewModel
    {
        [Display(Name = "編號")]
        public long NewsNum { get; set; }

		[Display(Name = "分類")]
        public string NewsClassName { get; set; }

        [Display(Name = "標題")]
        public string NewsTitle { get; set; }

        [Display(Name = "狀態")]
        public int NewsPublish { get; set; }

        [Display(Name = "新增時間")]
        public DateTime CreateTime { get; set; }

        [Display(Name = "更新時間")]
        public DateTime? EditTime { get; set; }
    }

}
