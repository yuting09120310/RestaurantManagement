using Dapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.BackEnd.ViewModel.ProductClass;
using RestaurantManagement.Models;
using System.Data;

namespace RestaurantManagement.BackEnd.Controllers
{
    public class ProductClassController : GenericController
    {
		private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IDbConnection _dbConnection;

        public ProductClassController(IDbConnection dbConnection, IWebHostEnvironment hostingEnvironment) : base(dbConnection) 
		{ 
			_hostingEnvironment = hostingEnvironment;
            _dbConnection = dbConnection;
        }


        public async Task<ActionResult> Index()
        {
            string strSQL = @"SELECT ProductClassId, ProductClassName, Description
                              FROM ProductClass";


            _dbConnection.Open();
            List<ProductClass> productClasses = (await _dbConnection.QueryAsync<ProductClass>(strSQL)).ToList();
            _dbConnection.Close();


            List<ProductClassIndexViewModel> indexViewModels = new ();
            foreach (ProductClass item in productClasses)
            {
                ProductClassIndexViewModel indexViewModel = new ()
                {
                    ProductClassId = item.ProductClassId,
                    ProductClassName = item.ProductClassName,
                    Description = item.Description,
                };
                indexViewModels.Add(indexViewModel);
            }

            return View(indexViewModels);
        }


        public ActionResult Create()
        {
            ProductClassCreateViewModel viewModel = new ();

            return View(viewModel);
        }


        [HttpPost]
        public async Task<ActionResult> Create(ProductClassCreateViewModel createViewModel)
        {
            if (ModelState.IsValid)
            {
                string strSQL = " INSERT INTO ProductClass (ProductClassName, Description) VALUES " +
                            $" ('{createViewModel.ProductClassName}','{createViewModel.Description}')";


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
            _dbConnection.Open();

            string strSQL = $"SELECT TOP 1 ProductClassId, ProductClassName, Description FROM ProductClass Where ProductClassId = {id}";
            ProductClass productClass = await _dbConnection.QueryFirstAsync<ProductClass>(strSQL);

            ProductClassEditViewModel editViewModel = new ()
            {
                ProductClassId = productClass.ProductClassId,
                ProductClassName = productClass.ProductClassName,
                Description= productClass.Description,
            };

            _dbConnection.Close();

            return View(editViewModel);
        }


        [HttpPost]
        public async Task<ActionResult> Edit(ProductClassEditViewModel editViewModel)
		{
            if(ModelState.IsValid)
            {
                string strSQL = "UPDATE ProductClass ";
                strSQL += $"SET ProductClassName = '{editViewModel.ProductClassName}', ";
                strSQL += $"Description = '{editViewModel.Description}' ";
                strSQL += $"WHERE ProductClassId = {editViewModel.ProductClassId}";

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

            string strSQL = $"DELETE FROM ProductClass WHERE ProductClassId = {id}";
            await _dbConnection.ExecuteAsync(strSQL);
            _dbConnection.Close();

            return Json("刪除完成");
        }


        
    }
}
