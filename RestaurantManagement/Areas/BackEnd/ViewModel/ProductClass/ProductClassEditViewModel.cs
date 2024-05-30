using System.ComponentModel.DataAnnotations;
using System.Data;

namespace RestaurantManagement.BackEnd.ViewModel.ProductClass
{
	public class ProductClassEditViewModel
    {
        public long ProductClassId { get; set; }


        [Required(ErrorMessage = "請輸入標題")]
        [Display(Name = "標題")]
        public string ProductClassName { get; set; }


        [Display(Name = "描述")]
        public string? Description { get; set; }
    }
}