using System.ComponentModel.DataAnnotations;
using System.Data;

namespace RestaurantManagement.BackEnd.ViewModel.News
{
	public class NewsEditViewModel
	{
		[Display(Name = "編號")]
		public long NewsNum { get; set; }


		[Required(ErrorMessage = "請選擇分類")]
		[Display(Name = "分類")]
		public int NewsClassNum { get; set; }


		[Required(ErrorMessage = "請輸入標題")]
		[Display(Name = "標題")]
		public string NewsTitle { get; set; }


		[Display(Name = "圖片")]
		public IFormFile? NewsImg { get; set; }


		[Required(ErrorMessage = "請輸入描述")]
		[Display(Name = "描述")]
		public string NewsDescription { get; set; }


		[Required(ErrorMessage = "請輸入內容")]	
		[Display(Name = "內容")]
		public string NewsContent { get; set; }


		[Display(Name = "排序")]
		public int? Sort { get; set; }


		[Display(Name = "上架日期")]
		public DateTime NewsPutTime { get; set; }


		[Display(Name = "下架日期")]
		public DateTime NewsOffTime { get; set; }


		[Required(ErrorMessage = "請選擇狀態")]
		[Display(Name = "狀態")]
		public int NewsPublish { get; set; }


		[Display(Name = "編輯人")]
		public int Editor { get; set; }


		[Display(Name = "編輯時間")]
		public DateTime EditTime { get; set; }
		
	}
}