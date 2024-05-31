using Dapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Models;
using System.Data;

namespace RestaurantManagement.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {

        private readonly IDbConnection _dbConnection;


        public MenuController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        [HttpGet]
        public async Task<ActionResult> Get()
        {
            string strSQL = "SELECT * FROM Product";

            _dbConnection.Open();
            List<Product> products = (await _dbConnection.QueryAsync<Product>(strSQL)).ToList();
            _dbConnection.Close();

            return Ok(products);
        }
    }
}
