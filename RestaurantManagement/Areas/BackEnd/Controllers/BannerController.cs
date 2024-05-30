using RestaurantManagement.BackEnd.ViewModel.Banner;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Models;
using System.Data;

namespace RestaurantManagement.BackEnd.Controllers
{
    public class BannerController : GenericController
    {
		private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IDbConnection _dbConnection;

        public BannerController(IDbConnection dbConnection, IWebHostEnvironment hostingEnvironment) : base(dbConnection) 
		{ 
			_hostingEnvironment = hostingEnvironment;
            _dbConnection = dbConnection;
        }


        public async Task<ActionResult> Index()
        {
            string strSQL = @"SELECT BannerId, BannerTitle, BannerPublish, CreateTime, EditTime
                              FROM Banner ";


            _dbConnection.Open();
            List<Banner> banners = (await _dbConnection.QueryAsync<Banner>(strSQL)).ToList();
            _dbConnection.Close();

            List<BannerIndexViewModel> indexViewModels = new ();
            foreach (Banner item in banners)
            {
                BannerIndexViewModel indexViewModel = new ()
                {
                    BannerId = item.BannerId,
                    BannerTitle = item.BannerTitle,
                    BannerPublish = item.BannerId,
                    CreateTime = Convert.ToDateTime(item.CreateTime),
                    EditTime = DateTime.TryParse(item.EditTime.ToString(), out var parsedTime) ? parsedTime : (DateTime?)null,
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
        public async Task<ActionResult> Create(BannerCreateViewModel createViewModel)
        {
            if (ModelState.IsValid)
            {
				var direPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", "Banner");

                if (!Directory.Exists(direPath))
                {
                    Directory.CreateDirectory(direPath);
                }

                var filePath = Path.Combine(direPath, createViewModel.BannerImg.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    createViewModel.BannerImg.CopyTo(fileStream);
                }


                string strSQL = " INSERT INTO Banner (BannerTitle, BannerDescription, BannerImg1, BannerPublish, BannerPutTime, BannerOffTime, CreateTime, Creator) VALUES " +
                            $" ('{createViewModel.BannerTitle}', '{createViewModel.BannerDescription}', '{createViewModel.BannerImg.FileName}', '{createViewModel.BannerPublish}', '{Convert.ToDateTime(createViewModel.BannerPutTime):yyyy-MM-dd HH:mm:ss}', '{Convert.ToDateTime(createViewModel.BannerOffTime):yyyy-MM-dd HH:mm:ss}', '{DateTime.Now:yyyy-MM-dd HH:mm:ss}', '{Convert.ToInt32(HttpContext.Session.GetString("AdminId"))}')";

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
			string path = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", "Banner");

            _dbConnection.Open();

            string strSQL = $"SELECT TOP 1 BannerId, BannerTitle, BannerImg1, BannerDescription, BannerSort, BannerPutTime, BannerOffTime, BannerPublish, Editor, EditTime FROM Banner Where BannerId = {id}";
            Banner banner = await _dbConnection.QueryFirstAsync<Banner>(strSQL);

            string filepath = Path.Combine(path, banner.BannerImg1);

            byte[] fileData = System.IO.File.ReadAllBytes(filepath);

            IFormFile formFile = new FormFile(new MemoryStream(fileData), 0, fileData.Length, "BannerImg", Path.GetFileName(filepath));


            BannerEditViewModel editViewModel = new ()
            {
                BannerId = banner.BannerId,
                BannerTitle = banner.BannerTitle,
                BannerImg = formFile,
                BannerDescription = banner.BannerDescription,
                Sort = int.TryParse(banner.BannerSort.ToString(), out var parseNum) ? parseNum : 0,
                BannerPutTime = DateTime.TryParse(banner.BannerPutTime.ToString(), out DateTime result) ? result : DateTime.Now,
                BannerOffTime = DateTime.TryParse(banner.BannerOffTime.ToString(), out DateTime results) ? results : DateTime.Now,
                BannerPublish = banner.BannerPublish
            };

            _dbConnection.Close();

            return View(editViewModel);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(BannerEditViewModel editViewModel)
		{
            if(ModelState.IsValid)
            {
                string strSQL = string.Empty;

                var direPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", "Banner");
				if (editViewModel.BannerImg != null)
				{
                    strSQL = $"SELECT BannerImg1 From Banner WHERE BannerId = '{editViewModel.BannerId}'";
                    _dbConnection.Open();
                    Banner banner = await _dbConnection.QueryFirstAsync<Banner>(strSQL);
                    _dbConnection.Close();

                    if (banner != null)
                    {
                        string fileName = banner.BannerImg1;
                        string oldfilePath = Path.Combine(direPath, fileName!);

                        if (System.IO.File.Exists(oldfilePath))
                        {
                            System.IO.File.Delete(oldfilePath);
                        }
                    }

                    if (!Directory.Exists(direPath))
                    {
                        Directory.CreateDirectory(direPath);
                    }

                    var NewfilePath = Path.Combine(direPath, editViewModel.BannerImg.FileName);
                    using var fileStream = new FileStream(NewfilePath, FileMode.Create);
                    editViewModel.BannerImg.CopyTo(fileStream);
                }


                strSQL = " UPDATE Banner SET ";
                strSQL += $" BannerTitle = '{editViewModel.BannerTitle}', ";
                strSQL += $" BannerDescription = '{editViewModel.BannerDescription}', ";

                if (editViewModel.BannerImg != null)
                {
                    strSQL += $"BannerImg1 = '{editViewModel.BannerImg.FileName}', ";
                }

                strSQL += $"BannerPublish = '{editViewModel.BannerPublish}', ";
                strSQL += $"BannerPutTime = '{Convert.ToDateTime(editViewModel.BannerPutTime):yyyy-MM-dd HH:mm:ss}', ";
                strSQL += $"BannerOffTime = '{Convert.ToDateTime(editViewModel.BannerOffTime):yyyy-MM-dd HH:mm:ss}', ";
                strSQL += $"EditTime = '{DateTime.Now:yyyy-MM-dd HH:mm:ss}', ";
                strSQL += $"Editor = '{Convert.ToInt32(HttpContext.Session.GetString("AdminId"))}' ";
                strSQL += $"WHERE BannerId = {editViewModel.BannerId}";

                _dbConnection.Open();

                await _dbConnection.ExecuteAsync(strSQL);

                _dbConnection.Close();


                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(editViewModel);
            }
			
		}


        public async Task<ActionResult> Delete(int id)
        {
            _dbConnection.Open();

            string strSQL = $"DELETE FROM Banner WHERE BannerId = {id}";
            await _dbConnection.ExecuteAsync(strSQL);

            strSQL = $"DELETE FROM Banner WHERE BannerId = {id}";
            await _dbConnection.ExecuteAsync(strSQL);

            _dbConnection.Close();

            return Json("刪除完成");
        }


        
    }
}
