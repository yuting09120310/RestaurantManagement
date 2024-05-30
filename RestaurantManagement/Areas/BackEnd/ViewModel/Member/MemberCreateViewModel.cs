using System.ComponentModel.DataAnnotations;
using System.Data;

namespace RestaurantManagement.BackEnd.ViewModel.Member
{
    public class MemberCreateViewModel
    {

		[Required(ErrorMessage = "請輸入帳號")]
		[Display(Name = "帳號")]
        public string MemberAccount { get; set; }


        [Required(ErrorMessage = "請輸入密碼")]
		[Display(Name = "密碼")]
		public string MemberPassword { get; set; }


		[Required(ErrorMessage = "請輸入姓名")]
		[Display(Name = "姓名")]
		public string MemberName { get; set; }


		[Required(ErrorMessage = "請輸入電話")]
		[Display(Name = "電話")]
		public string MemberPhone { get; set; }


		[Required(ErrorMessage = "請輸入信箱")]
		[Display(Name = "信箱")]
		public string MemberEmail { get; set; }


		[Required(ErrorMessage = "請選擇狀態")]
        [Display(Name = "狀態")]
        public int MemberPublish { get; set; }


		[Display(Name = "建立時間")]
		public DateTime CreateTime { get; set; }


		[Display(Name = "建立人")]
		public int Creator { get; set; }
	}

}
