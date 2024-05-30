using System.ComponentModel.DataAnnotations;

namespace RestaurantManagement.BackEnd.ViewModel.Admin
{
	public class AdminIndexViewModel
    {
        [Display(Name = "編號")]
        public long AdminId { get; set; }

		[Display(Name = "角色名稱")]
        public string? GroupName { get; set; }

        [Display(Name = "帳號")]
        public string? AdminAcc { get; set; }

        [Display(Name = "姓名")]
        public string? AdminName { get; set; }

        [Display(Name = "狀態")]
        public int? AdminPublish { get; set; }
    }

}
