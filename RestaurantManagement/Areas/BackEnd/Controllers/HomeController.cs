using RestaurantManagement.BackEnd.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace RestaurantManagement.Controllers
{
	public class HomeController : GenericController
    {

        public HomeController(IDbConnection dbConnection) : base(dbConnection) { }


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}