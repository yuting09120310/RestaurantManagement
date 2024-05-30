using RestaurantManagement.Areas.BackEnd.Attribute;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RestaurantManagement.Models;
using System.Data;

namespace RestaurantManagement.BackEnd.Controllers
{
    [Auth]
	[Area("BackEnd")]
    public class GenericController : Controller
    {

        private readonly IDbConnection _dbConnection;


        public GenericController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            GetMenu().Wait();
        }


        public async Task GetMenu()
        {
            _dbConnection.Open();

            int GroupId = Convert.ToInt16(HttpContext.Session.GetString("GroupId"));

            string moduleQuery = @"SELECT *
                               FROM MenuGroup
                               WHERE MenuGroupPublish = 1
                               ORDER BY MenuGroupId ASC";

            IEnumerable<MenuGroup> module = await _dbConnection.QueryAsync<MenuGroup>(moduleQuery);
            ViewBag.module = module.ToList();

            string moduleFunQuery = @"SELECT c.*
                                  FROM MenuSub c
                                  JOIN AdminRole s ON c.MenuSubId = s.MenuSubId
                                  WHERE c.MenuSubPublish = 1 AND s.GroupId = @GroupId";

            IEnumerable<MenuSub> moduleFun = await _dbConnection.QueryAsync<MenuSub>(moduleFunQuery, new { GroupId });
            ViewBag.moduleFun = moduleFun.ToList();

            _dbConnection.Close();

            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminId = HttpContext.Session.GetString("AdminId");
        }
    }
}
