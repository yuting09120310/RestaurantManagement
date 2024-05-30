using System.ComponentModel.DataAnnotations;
using System.Data;

namespace RestaurantManagement.BackEnd.ViewModel.Banner
{
    public class BannerCreateViewModel
    {

		[Required(ErrorMessage = "請輸入標題")]
		[Display(Name = "標題")]
        public string BannerTitle { get; set; }


        [Required(ErrorMessage = "請上傳圖片")]
        [Display(Name = "圖片")]
        public IFormFile BannerImg { get; set; }


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


		[Display(Name = "建立人")]
		public int Creator { get; set; }
	}

}
