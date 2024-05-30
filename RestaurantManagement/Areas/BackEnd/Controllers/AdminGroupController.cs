using RestaurantManagement.BackEnd.ViewModel.AdminGroup;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Models;
using System.Data;

namespace RestaurantManagement.BackEnd.Controllers
{
    public class AdminGroupController : GenericController
    {
        private readonly IDbConnection _dbConnection;

        public AdminGroupController(IDbConnection dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public async Task<ActionResult> Index()
        {
            string strSQL = "SELECT GroupId, GroupName, GroupPublish, CreateTime FROM AdminGroup WHERE GroupPublish = 1";


            _dbConnection.Open();
            List<AdminGroup> adminGroups = (await _dbConnection.QueryAsync<AdminGroup>(strSQL)).ToList();
            _dbConnection.Close();


            List<AdminGroupIndexViewModel> indexViewModels = new();
            foreach (AdminGroup adminGroup in adminGroups)
            {
                AdminGroupIndexViewModel indexViewModel = new ()
                {
                    GroupId = adminGroup.GroupId,
                    GroupName = adminGroup.GroupName,
                    AdminPublish = adminGroup.GroupPublish,
                    CreateTime = adminGroup.CreateTime,
                };
                indexViewModels.Add(indexViewModel);
            }

            return View(indexViewModels);
        }


        public async Task<ActionResult> Create()
        {
            _dbConnection.Open();

            string sqlMenuGroup = "SELECT MenuGroupId, MenuGroupName, MenuGroupIcon FROM MenuGroup";
            string sqlMenuSub = "SELECT MenuSubId,MenuGroupId, MenuSubId, MenuSubName FROM MenuSub";

            AdminGroupCreateViewModel createViewModel = new ()
            {
                GroupName = "",
                GroupInfo = "",
                GroupPublish = 1,

                MenuGroupModels = (await _dbConnection.QueryAsync<MenuGroup>(sqlMenuGroup)).ToList(),
                MenuSubModels = (await _dbConnection.QueryAsync<MenuSub>(sqlMenuSub)).ToList(),
                AdminRoleModels = new (),
            };


            _dbConnection.Close();

            return View(createViewModel);
        }


        [HttpPost]
        public async Task<ActionResult> Create(IFormCollection Collection)
        {
            _dbConnection.Open();

            string strSQL = $"INSERT INTO AdminGroup (GroupName, GroupInfo, GroupPublish, CreateTime, Creator) VALUES ('{Collection["GroupName"]}', '{Collection["GroupInfo"]}', '1', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}', {HttpContext.Session.GetString("AdminId")}) ; SELECT SCOPE_IDENTITY();";
            int GroupId = await _dbConnection.QuerySingleAsync<int>(strSQL);

            Dictionary<string, string> roleDicts = Collection
                 .Where(kv => kv.Key.StartsWith("e"))
                 .Select(kv => new KeyValuePair<string, string>(kv.Key.Split('_')[1], kv.Value!))
                 .ToDictionary(kv => kv.Key, kv => kv.Value);

            foreach (string roleDict in roleDicts.Keys)
            {
                strSQL = $"INSERT INTO AdminRole (GroupId, MenuSubId, Role, CreateTime, Creator) VALUES ('{GroupId}', '{roleDict}', '{roleDicts[roleDict]}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}', '1')";
                await _dbConnection.ExecuteAsync(strSQL);
            }

            _dbConnection.Close();

            return RedirectToAction(nameof(Index));
        }


        public async Task<ActionResult> Edit(int id)
        {
            _dbConnection.Open();

            string sqlMenuGroup = "SELECT MenuGroupId, MenuGroupName, MenuGroupIcon FROM MenuGroup";
            string sqlMenuSub = "SELECT MenuSubId,MenuGroupId, MenuSubId, MenuSubName FROM MenuSub";
            string sqlRole = $"SELECT RoleId, GroupId, MenuSubId, Role FROM AdminRole WHERE GroupId = {id}";


            string sqlGroup = $"SELECT GroupName, GroupInfo FROM AdminGroup WHERE GroupId = {id}";
            AdminGroup adminGroup = await _dbConnection.QueryFirstAsync<AdminGroup>(sqlGroup);


            AdminGroupCreateViewModel createViewModel = new ()
            {
                CreatorName = HttpContext.Session.GetString("AdminName")!,
                Creator = Convert.ToInt32(HttpContext.Session.GetString("AdminId")),
                GroupName = adminGroup.GroupName,
                GroupInfo = adminGroup.GroupInfo,
                GroupPublish = 1,

                MenuGroupModels = (await _dbConnection.QueryAsync<MenuGroup>(sqlMenuGroup)).ToList(),
                MenuSubModels = (await _dbConnection.QueryAsync<MenuSub>(sqlMenuSub)).ToList(),
                AdminRoleModels = (await _dbConnection.QueryAsync<AdminRole>(sqlRole)).ToList(),
            };


            _dbConnection.Close();

            return View(createViewModel);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(long id, IFormCollection Collection)
        {
            try
            {
                Dictionary<string, string> roleDicts = Collection
                 .Where(kv => kv.Key.StartsWith("e"))
                 .Select(kv => new KeyValuePair<string, string>(kv.Key.Split('_')[1], kv.Value!))
                 .ToDictionary(kv => kv.Key, kv => kv.Value);

                int GroupId = Convert.ToInt32(id);

                _dbConnection.Open();


                foreach (string roleDict in roleDicts.Keys)
                {
                    long MenuSubId = Convert.ToInt64(roleDict);
                    string strRole = $"SELECT * FROM AdminRole WHERE GroupId = '{GroupId}' AND MenuSubId = '{MenuSubId}'";
                    AdminRole adminRole = _dbConnection.QueryFirstOrDefault<AdminRole>(strRole)!;

                    string strSQL = string.Empty;

                    if (adminRole != null)
                    {
                        strSQL = $"UPDATE AdminRole SET Role = '{roleDicts[roleDict]}' WHERE GroupId = '{GroupId}' AND MenuSubId = '{MenuSubId}'";
                    }
                    else
                    {
                        strSQL = $"INSERT INTO AdminRole (GroupId, MenuSubId, Role, CreateTime, Creator) VALUES ('{GroupId}', '{MenuSubId}', '{roleDicts[roleDict]}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}', '1')";
                    }

                    await _dbConnection.ExecuteAsync(strSQL);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public async Task<ActionResult> Delete(int id)
        {
            _dbConnection.Open();

            string strSQL = $"DELETE FROM AdminGroup WHERE GroupId = {id}";
            await _dbConnection.ExecuteAsync(strSQL);

            strSQL = $"DELETE FROM AdminRole WHERE GroupId = {id}";
            await _dbConnection.ExecuteAsync(strSQL);

            _dbConnection.Close();

            return Json("刪除完成");
        }

    }
}
