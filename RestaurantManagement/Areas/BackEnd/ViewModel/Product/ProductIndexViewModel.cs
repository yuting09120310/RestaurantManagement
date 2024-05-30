using System.ComponentModel.DataAnnotations;
using System.Data;

namespace RestaurantManagement.BackEnd.ViewModel.Product
{
    public class ProductIndexViewModel
	{
        [Display(Name = "編號")]
        public int ProductId { get; set; }

		[Display(Name = "類別")]
        public string ProductClassName { get; set; }

        [Display(Name = "產品名稱")]
        public string ProductName { get; set; }

        [Display(Name = "圖片")]
        public string ProductImg1 { get; set; }


        [Display(Name = "價格")]
        public decimal Price { get; set; }
    }

}
