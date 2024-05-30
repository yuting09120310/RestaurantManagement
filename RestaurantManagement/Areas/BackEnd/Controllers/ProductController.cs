using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantManagement.BackEnd.ViewModel.Product;
using RestaurantManagement.Models;
using System.Data;

namespace RestaurantManagement.BackEnd.Controllers
{
    public class ProductController : GenericController
    {
		private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IDbConnection _dbConnection;

        public ProductController(IDbConnection dbConnection, IWebHostEnvironment hostingEnvironment) : base(dbConnection) 
		{ 
			_hostingEnvironment = hostingEnvironment;
            _dbConnection = dbConnection;
        }


        public async Task<ActionResult> Index()
        {
            string strSQL = @"SELECT p.ProductId, p.ProductName, p.Description, p.Price, p.ProductClassId, p.ProductImg1, pc.ProductClassId, pc.ProductClassName
                  FROM Product as p
                  LEFT JOIN ProductClass as pc
                  ON p.ProductClassId = pc.ProductClassId";


            _dbConnection.Open();
            var result = await _dbConnection.QueryAsync<Product, ProductClass, dynamic>(
                 strSQL,
                 (product, productClass) => new
                 {
                     ProductId = product.ProductId,
                     ProductName = product.ProductName,
                     Price = product.Price,
                     ProductImg1 = product.ProductImg1,
                     ProductClassName = productClass.ProductClassName,
                 },
                 splitOn: "ProductClassId"
             );
            _dbConnection.Close();


            var indexViewModels = result.Select(x => new ProductIndexViewModel
            {
                ProductId = x.ProductId,
                ProductClassName = x.ProductClassName,
                ProductName = x.ProductName,
                Price= x.Price,
                ProductImg1 = x.ProductImg1,
            });

            return View(indexViewModels);
        }


        public async Task<ActionResult> Create()
        {
            await GetType();

            ProductCreateViewModel createViewModel = new ();

            return View(createViewModel);
        }


        [HttpPost]
        public async Task<ActionResult> Create(ProductCreateViewModel createViewModel)
        {
            if (ModelState.IsValid)
            {
				var direPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", "Product");

                if (!Directory.Exists(direPath))
                {
                    Directory.CreateDirectory(direPath);
                }

                var filePath = Path.Combine(direPath, createViewModel.ProductImg1.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    createViewModel.ProductImg1.CopyTo(fileStream);
                }

                string strSQL = " INSERT INTO Product (ProductClassId, ProductName, Description, ProductImg1, Price) VALUES " +
                            $" ('{createViewModel.ProductClassId}', '{createViewModel.ProductName}', '{createViewModel.Description}', '{createViewModel.ProductImg1.FileName}', '{createViewModel.Price}')";

                _dbConnection.Open();

                await _dbConnection.ExecuteAsync(strSQL);

                _dbConnection.Close();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                await GetType();

                return View(createViewModel);
            }
        }


        public async Task<ActionResult> Edit(int id)
        {
            await GetType();

            string path = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", "Product");

            _dbConnection.Open();
            string strSQL = $"SELECT TOP 1 ProductId, ProductName, Description, ProductImg1, Price, ProductClassId FROM Product Where ProductId = {id}";
            Product product =  await _dbConnection.QueryFirstAsync<Product>(strSQL);
            _dbConnection.Close();

            // 從資料庫中獲取檔案路徑
            string filepath = Path.Combine(path, product.ProductImg1);

            // 讀取檔案數據
            byte[] fileData = System.IO.File.ReadAllBytes(filepath);

            // 創建 FormFile 物件
            IFormFile formFile = new FormFile(new MemoryStream(fileData), 0, fileData.Length, "ProductImg", Path.GetFileName(filepath));


            ProductEditViewModel editViewModel = new ()
            {
                ProductId = product.ProductId,
                ProductClassId = product.ProductClassId,
                ProductName = product.ProductName,
                ProductImg = formFile,
                Description = product.Description,
                Price = product.Price,
            };

            return View(editViewModel);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(ProductEditViewModel editViewModel)
		{
            if(ModelState.IsValid)
            {
                string strSQL = string.Empty;


                var direPath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", "Product");
				if (editViewModel.ProductImg != null)
				{
                    strSQL = $"SELECT ProductImg1 From Product WHERE ProductId = '{editViewModel.ProductId}'";
                    _dbConnection.Open();
                    Product product = await _dbConnection.QueryFirstAsync<Product>(strSQL);
                    _dbConnection.Close();

                    if (product != null)
                    {
                        string fileName = product.ProductImg1;
                        string oldfilePath = Path.Combine(direPath, fileName);

                        if (System.IO.File.Exists(oldfilePath))
                        {
                            System.IO.File.Delete(oldfilePath);
                        }
                    }

                    if (!Directory.Exists(direPath))
                    {
                        Directory.CreateDirectory(direPath);
                    }

                    var filePath = Path.Combine(direPath, editViewModel.ProductImg.FileName);
                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    editViewModel.ProductImg.CopyTo(fileStream);
                }

                strSQL = "UPDATE Product ";
                strSQL += $"SET ProductClassId = '{editViewModel.ProductClassId}', ";
                strSQL += $"ProductName = '{editViewModel.ProductName}', ";
                strSQL += $"Description = '{editViewModel.Description}', ";

                if (editViewModel.ProductImg != null)
                {
                    strSQL += $"ProductImg1 = '{editViewModel.ProductImg.FileName}', ";
                }

                strSQL += $"Price = '{editViewModel.Price}' ";
                strSQL += $"WHERE ProductId = {editViewModel.ProductId}";

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

            string strSQL = $"DELETE FROM Product WHERE ProductId = {id}";
            await _dbConnection.ExecuteAsync(strSQL);

            _dbConnection.Close();

            return Json("刪除完成");
        }


        public new async Task GetType()
        {
            string strSQL = "SELECT ProductClassId,ProductClassName FROM ProductClass";
            _dbConnection.Open();
            List<ProductClass> productClasses = (await _dbConnection.QueryAsync<ProductClass>(strSQL)).ToList();
            _dbConnection.Close();

            List<SelectListItem> adminGroup = new();
            foreach (ProductClass item in productClasses)
            {
                adminGroup.Add(new SelectListItem { Text = item.ProductClassName, Value = item.ProductClassId.ToString() });
            }

            ViewBag.adminGroup = adminGroup;
        }
    }
}
