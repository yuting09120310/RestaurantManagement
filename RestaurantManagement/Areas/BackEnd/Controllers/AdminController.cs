using RestaurantManagement.BackEnd.ViewModel.Admin;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantManagement.Models;
using System.Data;

namespace RestaurantManagement.BackEnd.Controllers
{
    public class AdminController : GenericController
    {

        private readonly IDbConnection _dbConnection;

        public AdminController(IDbConnection dbConnection) : base(dbConnection) 
        {
            _dbConnection = dbConnection;
        }


        public async Task<ActionResult> Index()
        {
            string strSQL = @$"SELECT a.AdminId, a.AdminAcc, a.AdminName, a.AdminPublish, g.GroupId, g.GroupName
                               FROM Admin a
                               LEFT JOIN AdminGroup g ON a.GroupId = g.GroupId
			";
            
            _dbConnection.Open();
            var result = await _dbConnection.QueryAsync<Admin, AdminGroup, dynamic>(
                 strSQL,
                 (admin, group) => new
                 {
                     AdminId = admin.AdminId,
                     AdminAcc = admin.AdminAcc,
                     AdminName = admin.AdminName,
                     AdminPublish = admin.AdminPublish,
                     GroupName = group?.GroupName
                 },
                 splitOn: "GroupId"
             );
            _dbConnection.Close();

            var indexViewModels = result.Select(x => new AdminIndexViewModel
            {
                AdminId = x.AdminId,
                GroupName = x.GroupName,
                AdminAcc = x.AdminAcc,
                AdminName = x.AdminName,
                AdminPublish = x.AdminPublish
            }).ToList();

            return View(indexViewModels);
        }


        public async Task<ActionResult> Create()
        {
            string strSQL = "SELECT GroupId,GroupName FROM admingroup WHERE GroupPublish = 1";
            _dbConnection.Open();
            List<AdminGroup> adminGroups = (await _dbConnection.QueryAsync<AdminGroup>(strSQL)).ToList();
            _dbConnection.Close();

            List<SelectListItem> adminGroup = new();
            foreach (AdminGroup item in adminGroups)
            {
                adminGroup.Add(new SelectListItem { Text = item.GroupName, Value = item.GroupId.ToString() });
            }

            ViewBag.adminGroup = adminGroup;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AdminCreateViewModel createViewModel)
        {
            if (ModelState.IsValid)
            {
                string strSQL = @$"INSERT INTO Admin ( GroupId, AdminAcc, AdminName, AdminPwd, AdminPublish,    Creator) 
                                    VALUES ('{createViewModel.GroupId}' , '{createViewModel.AdminAcc}' ,'{createViewModel.AdminName}' , 
                                    '{createViewModel.AdminPwd}' , '{createViewModel.AdminPublish}', '{Convert.ToInt32(HttpContext.Session.GetString("AdminId"))}' )";

                _dbConnection.Open();
                await _dbConnection.ExecuteAsync(strSQL);
                _dbConnection.Close();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(createViewModel);
            }
        }


        public async Task<ActionResult> Edit(int id)
        {
            string strSQL = "SELECT GroupId,GroupName FROM admingroup WHERE GroupPublish = 1";
            _dbConnection.Open();
            List<AdminGroup> adminGroups = (await _dbConnection.QueryAsync<AdminGroup>(strSQL)).ToList();
            _dbConnection.Close();

            List<SelectListItem> adminGroup = new();
            foreach (AdminGroup item in adminGroups)
            {
                adminGroup.Add(new SelectListItem { Text = item.GroupName, Value = item.GroupId.ToString() });
            }

            ViewBag.adminGroup = adminGroup;


            strSQL = $"SELECT TOP 1 *  FROM Admin WHERE AdminId = {id}";
            _dbConnection.Open();
            Admin admin  = await _dbConnection.QueryFirstOrDefaultAsync<Admin>(strSQL);
            _dbConnection.Close();


            AdminEditViewModel editViewModel = new()
            {
                AdminId = admin.AdminId,
                AdminAcc = admin.AdminAcc,
                AdminName = admin.AdminName,
            };

            return View(editViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AdminEditViewModel editViewModel)
        {
            try
            {
                string strSQL = @$"UPDATE Admin
                                SET AdminAcc = '{editViewModel.AdminAcc}', AdminName = '{editViewModel.AdminName}', AdminPwd = '{editViewModel.AdminPwd}' , Editor = '{Convert.ToInt32(HttpContext.Session.GetString("AdminId"))}'
                                WHERE AdminId = '{editViewModel.AdminId}'";

                _dbConnection.Open();
                await _dbConnection.ExecuteAsync(strSQL);
                _dbConnection.Close();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public async Task<ActionResult> Delete(int id)
        {
            string strSQL = $"DELETE FROM Admin WHERE AdminId = {id}";

            _dbConnection.Open();
            await _dbConnection.ExecuteAsync(strSQL);
            _dbConnection.Close();

            return Json("刪除完成");
        }

    }
}
