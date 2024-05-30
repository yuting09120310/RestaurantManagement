using System.ComponentModel.DataAnnotations;
using System.Data;

namespace RestaurantManagement.BackEnd.ViewModel.NewsClass
{
    public class NewsClassIndexViewModel
    {
        [Display(Name = "編號")]
        public long NewsClassNum { get; set; }


        [Display(Name = "標題")]
        public string NewsClassName { get; set; }


        [Display(Name = "狀態")]
        public int NewsClassPublish { get; set; }


        [Display(Name = "新增時間")]
        public DateTime CreateTime { get; set; }
    }

}
