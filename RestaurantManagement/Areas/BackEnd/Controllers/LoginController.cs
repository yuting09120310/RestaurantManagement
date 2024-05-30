using Dapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Models;
using System.Data;

namespace RestaurantManagement.BackEnd.Controllers
{
    [Area("BackEnd")]
    public class LoginController : Controller 
    {

        private readonly IDbConnection _dbConnection;

        public LoginController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public IActionResult Index() {
            return View();
        }


        [HttpPost]
        public IActionResult Index(string accountNumber, string accountPassword) 
        {
            _dbConnection.Open();
            string sql = $"SELECT TOP 1 * FROM Admin WHERE AdminAcc = '{accountNumber}' AND AdminPwd = '{accountPassword}'";
            Admin admin = _dbConnection.QueryFirstOrDefault<Admin>(sql);
            _dbConnection.Close();


            if (admin != null)
            {
                HttpContext.Session.SetString("AdminId", admin.AdminId.ToString());
                HttpContext.Session.SetString("AdminName", admin.AdminName);
                HttpContext.Session.SetString("GroupId", admin.GroupId.ToString());

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "登入失敗，請檢察帳號密碼是否輸入錯誤";
                return View();
            }
        }
    }
}
