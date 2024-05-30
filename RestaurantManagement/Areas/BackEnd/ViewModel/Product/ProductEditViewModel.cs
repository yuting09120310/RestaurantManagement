using System.ComponentModel.DataAnnotations;
using System.Data;

namespace RestaurantManagement.BackEnd.ViewModel.Product
{
	public class ProductEditViewModel
	{
		[Display(Name = "編號")]
		public long ProductId { get; set; }


		[Required(ErrorMessage = "請選擇分類")]
		[Display(Name = "分類")]
		public int ProductClassId { get; set; }


		[Required(ErrorMessage = "請輸入標題")]
		[Display(Name = "標題")]
		public string ProductName { get; set; }


        [Required(ErrorMessage = "請輸入描述")]
        [Display(Name = "描述")]
        public string Description { get; set; }


        [Display(Name = "圖片")]
		public IFormFile? ProductImg { get; set; }


        [Required(ErrorMessage = "請輸入價格")]
        [Display(Name = "價格")]
        public decimal Price { get; set; }
    }
}