using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Dapper;
using System.Data;
using RestaurantManagement.Models;

namespace RestaurantManagement.Areas.BackEnd.Attribute
{
	public class AuthFilter : IAuthorizationFilter
	{
        private readonly IDbConnection _dbConnection;

        public AuthFilter(IDbConnection dbConnection)
		{
            _dbConnection = dbConnection;
        }

		public void OnAuthorization(AuthorizationFilterContext context)
		{

			string GroupId = context.HttpContext.Session.GetString("GroupId")!;
			string AdminName = context.HttpContext.Session.GetString("AdminName")!;
			string AdminId = context.HttpContext.Session.GetString("AdminId")!;

			if (string.IsNullOrEmpty(GroupId) || string.IsNullOrEmpty(AdminName) || string.IsNullOrEmpty(AdminId))
			{
				context.Result = new ContentResult()
				{
					Content = "<script>alert('尚未登入');window.location.href='/Backend/Login/Index'</script>",
					ContentType = "text/html;charset=utf-8",
				};

				return;
			}

			string controllerName = context.RouteData.Values["Controller"]!.ToString()!;
			string actionName = context.RouteData.Values["Action"]!.ToString()!;

			if (controllerName != "Home")
			{
                Dictionary<string, string> dic = new()
                {
                    { "Create", "C" },
                    { "Index", "R" },
                    { "Edit", "U" },
                    { "Delete", "D" }
                };

                string sqlMenuNum = $"SELECT MenuSubId FROM MenuSub WHERE MenuSubUrl Like '/BackEnd/{controllerName}/%'";
                _dbConnection.Open();
                MenuSub menuNum = _dbConnection.QueryFirstOrDefault<MenuSub>(sqlMenuNum)!;

                string sqlRole = $"SELECT * FROM AdminRole WHERE GroupId = {GroupId} AND MenuSubId = {menuNum.MenuSubId} AND Role LIKE '%{dic[actionName!]}%'";
				AdminRole adminRole = _dbConnection.QueryFirstOrDefault<AdminRole>(sqlRole)!;

                _dbConnection.Close();


				if (adminRole == null)
				{
					context.Result = new ContentResult()
					{
						Content = "<script>alert('權限不足');history.back()</script>",
						ContentType = "text/html;charset=utf-8",
					};

					return;
				}
			}
		}
	}
}
