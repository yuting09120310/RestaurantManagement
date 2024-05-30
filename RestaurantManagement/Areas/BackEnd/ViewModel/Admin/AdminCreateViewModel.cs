using System.ComponentModel.DataAnnotations;

namespace RestaurantManagement.BackEnd.ViewModel.Admin
{
	public class AdminCreateViewModel
    {

		[Required(ErrorMessage = "請選擇群組")]
		[Display(Name = "群組編號")]
		public int GroupId { get; set; }

		[Required(ErrorMessage = "請輸入帳號")]
		[Display(Name = "帳號")]
        public string AdminAcc { get; set; }


        [Required(ErrorMessage = "請輸入密碼")]
        [Display(Name = "密碼")]
        public string AdminPwd { get; set; }


        [Required(ErrorMessage = "請輸入姓名")]
		[Display(Name = "姓名")]
		public string AdminName { get; set; }


        [Required(ErrorMessage = "請選擇狀態")]
        [Display(Name = "狀態")]
        public int AdminPublish { get; set; }



		public int Creator { get; set; }

	}

}
