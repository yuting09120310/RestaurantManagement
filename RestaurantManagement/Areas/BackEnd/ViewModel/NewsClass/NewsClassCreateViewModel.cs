using System.ComponentModel.DataAnnotations;
using System.Data;

namespace RestaurantManagement.BackEnd.ViewModel.NewsClass
{
    public class NewsClassCreateViewModel
    {
		[Required(ErrorMessage = "請輸入標題")]
		[Display(Name = "標題")]
        public string NewsClassName { get; set; }


		[Required(ErrorMessage = "請選擇狀態")]
        [Display(Name = "狀態")]
        public int NewsClassPublish { get; set; }
	}

}
