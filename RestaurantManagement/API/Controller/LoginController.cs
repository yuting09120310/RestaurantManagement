using Dapper;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.API.Dtos;
using RestaurantManagement.Models;
using System.Data;


namespace RestaurantManagement.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IDbConnection _dbConnection;


        public LoginController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        [HttpPost]
        public async Task<ActionResult> Post(LoginDto loginDto)
        {
            string strSQL = "SELECT * FROM Member WHERE MemberAccount = @MemberAccount AND MemberPassword = @MemberPassword";
            Member member = await  _dbConnection.QueryFirstAsync<Member>(strSQL, new { MemberAccount = loginDto.MemberAccount, MemberPassword = loginDto.MemberPassword });
            return Ok(member);
        }
        
    }
}
