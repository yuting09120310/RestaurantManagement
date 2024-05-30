using System.ComponentModel.DataAnnotations;
using System.Data;

namespace RestaurantManagement.BackEnd.ViewModel.AdminGroup
{
    public class AdminGroupIndexViewModel
    {
        [Display(Name = "編號")]
        public long GroupId { get; set; }

		[Display(Name = "群組名稱")]
        public string? GroupName { get; set; }

        [Display(Name = "狀態")]
        public int? AdminPublish { get; set; }

        [Display(Name = "建檔時間")]
        public DateTime? CreateTime { get; set; }
    }
}
