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
            try
            {
                string strSQL = "SELECT * FROM Member WHERE MemberAccount = @MemberAccount AND MemberPassword = @MemberPassword";
                Member member = await _dbConnection.QueryFirstOrDefaultAsync<Member>(strSQL, new { MemberAccount = loginDto.MemberAccount, MemberPassword = loginDto.MemberPassword });

                if (member == null)
                {
                    return Ok(new { success = false, message = "帳號或密碼錯誤。" });
                }

                return Ok(new { success = true, data = member });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "內部伺服器錯誤。" });
            }
        }


    }
}
