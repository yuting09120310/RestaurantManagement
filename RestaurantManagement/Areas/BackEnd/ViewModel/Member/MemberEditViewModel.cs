using System.ComponentModel.DataAnnotations;
using System.Data;

namespace RestaurantManagement.BackEnd.ViewModel.Member
{
	public class MemberEditViewModel
	{
		[Display(Name = "編號")]
		public int MemberId { get; set; }


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


		[Display(Name = "編輯時間")]
		public DateTime EditTime { get; set; }


		[Display(Name = "編輯人")]
		public int Editor { get; set; }
	}
}