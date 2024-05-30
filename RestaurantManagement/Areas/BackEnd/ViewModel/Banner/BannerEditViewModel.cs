using System.ComponentModel.DataAnnotations;
using System.Data;

namespace RestaurantManagement.BackEnd.ViewModel.Banner
{
	public class BannerEditViewModel
	{
		[Display(Name = "編號")]
		public long BannerId { get; set; }

		[Required(ErrorMessage = "請輸入標題")]
		[Display(Name = "標題")]
		public string BannerTitle { get; set; }


		[Display(Name = "圖片")]
		public IFormFile? BannerImg { get; set; }


		[Required(ErrorMessage = "請輸入描述")]
		[Display(Name = "描述")]
		public string BannerDescription { get; set; }


		[Display(Name = "排序")]
		public int? Sort { get; set; }


		[Display(Name = "上架日期")]
		public DateTime BannerPutTime { get; set; }


		[Display(Name = "下架日期")]
		public DateTime BannerOffTime { get; set; }


		[Required(ErrorMessage = "請選擇狀態")]
		[Display(Name = "狀態")]
		public int BannerPublish { get; set; }


		[Display(Name = "編輯人")]
		public int Editor { get; set; }


		[Display(Name = "編輯時間")]
		public DateTime EditTime { get; set; }
		
	}
}