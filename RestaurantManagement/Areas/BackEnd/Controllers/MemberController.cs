using RestaurantManagement.BackEnd.ViewModel.Member;
using RestaurantManagement.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace RestaurantManagement.BackEnd.Controllers
{
    public class MemberController : GenericController
    {
		private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IDbConnection _dbConnection;

        public MemberController(IDbConnection dbConnection, IWebHostEnvironment hostingEnvironment) : base(dbConnection) 
		{ 
			_hostingEnvironment = hostingEnvironment;
            _dbConnection = dbConnection;
        }


        public async Task<ActionResult> Index()
        {
            _dbConnection.Open();

            string memberQuery = @"SELECT MemberId, MemberAccount, MemberName, MemberPhone, MemberEmail
                              FROM Member ";

            var members = await _dbConnection.QueryAsync<Member>(memberQuery);
            _dbConnection.Close();


            List<MemberIndexViewModel> indexViewModels = new ();
            foreach (Member member in members)
            {
                MemberIndexViewModel indexViewModel = new ()
                {
                    MemberId = member.MemberId,
                    MemberAccount = member.MemberAccount,
                    MemberName = member.MemberName,
                    MemberPhone = member.MemberPhone,
                    MemberEmail = member.MemberEmail,
                };
                indexViewModels.Add(indexViewModel);
            }

            return View(indexViewModels);
        }


        public ActionResult Create()
        {
			return View();
        }


        [HttpPost]
        public ActionResult Create(MemberCreateViewModel createViewModel)
        {
            if (ModelState.IsValid)
            {
                string query = " INSERT INTO Member (MemberAccount, MemberPassword, MemberName, MemberPhone, MemberEmail, MemberPublish) VALUES " +
                            $" (@MemberAccount, @MemberPassword, @MemberName, @MemberPhone, @MemberEmail, @MemberPublish)";

                _dbConnection.Open();

                _dbConnection.QuerySingleAsync(query, createViewModel);

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
            _dbConnection.Open();

            string strSQL = $"SELECT TOP 1 MemberId, MemberAccount, MemberName, MemberPhone, MemberEmail, MemberPublish FROM Member Where MemberId = @Id";
            var member = await _dbConnection.QueryFirstOrDefaultAsync<Member>(strSQL, new { Id = id });

            MemberEditViewModel editViewModel = new ()
            {
                MemberId = member.MemberId,
                MemberAccount = member.MemberAccount,
                MemberName = member.MemberName,
                MemberPhone = member.MemberPhone,
                MemberEmail = member.MemberEmail,
                MemberPublish = member.MemberPublish
            };

            _dbConnection.Close();

            return View(editViewModel);
        }


        [HttpPost]
        public ActionResult Edit(MemberEditViewModel editViewModel)
		{
            if(ModelState.IsValid)
            {
                string strSQL = "UPDATE Member ";
                strSQL += $"MemberAccount = '{editViewModel.MemberAccount}', ";
                strSQL += $"MemberPassword = '{editViewModel.MemberPassword}', ";
                strSQL += $"MemberName = '{editViewModel.MemberName}', ";
                strSQL += $"MemberPhone = '{editViewModel.MemberPhone}', ";
                strSQL += $"MemberEmail = '{editViewModel.MemberEmail}', ";
                strSQL += $"MemberPublish = '{editViewModel.MemberPublish}', ";
                strSQL += $"WHERE MemberId = {editViewModel.MemberId}";

                _dbConnection.Open();

                _dbConnection.QuerySingleAsync<Member>(strSQL);

                _dbConnection.Close();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(editViewModel);
            }
			
		}


        public ActionResult Delete(int id)
        {
            _dbConnection.Open();

            string strSQL = $"DELETE FROM Member WHERE MemberId = @Id";
            _dbConnection.QuerySingleAsync(strSQL, new { Id = id});

            _dbConnection.Close();

            return Json("刪除完成");
        }


        
    }
}
