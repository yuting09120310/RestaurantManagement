using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace RestaurantManagement.BackEnd.Controllers
{

	public class LogoutController : GenericController {


		public LogoutController(IDbConnection dbConnection) : base(dbConnection) { }


		public IActionResult Index() {

            HttpContext.Session.Remove("AdminId");
            HttpContext.Session.Remove("AdminName");
            HttpContext.Session.Remove("GroupId");

            return RedirectToAction("Index", "Login");
        }

    }
}
