using System.ComponentModel.DataAnnotations;
using System.Data;

namespace RestaurantManagement.BackEnd.ViewModel.ProductClass
{
    public class ProductClassIndexViewModel
    {
        [Display(Name = "編號")]
        public long ProductClassId { get; set; }


        [Display(Name = "標題")]
        public string ProductClassName { get; set; }


        [Display(Name = "描述")]
        public string? Description { get; set; }
    }

}
