using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantManagement.API.Dtos;
using System;
using System.Data;
using System.Threading.Tasks;

namespace RestaurantManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<MemberController> _logger;

        public MemberController(IDbConnection dbConnection, ILogger<MemberController> logger)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [HttpPost("add")]
        public async Task<ActionResult> AddMember([FromBody] CreateMemberDto member)
        {
            if (member == null)
            {
                return BadRequest("會員數據無效");
            }

            try
            {
                string sql = @"
                    INSERT INTO [dbo].[Member] 
                    (MemberAccount, MemberPassword, MemberName, MemberPhone, MemberEmail, MemberPublish, LastLogin, IP)
                    VALUES 
                    (@MemberAccount, @MemberPassword, @MemberName, @MemberPhone, @MemberEmail, @MemberPublish, @LastLogin, @IP);
                    SELECT CAST(SCOPE_IDENTITY() as int);";

                _dbConnection.Open();
                var newMemberId = await _dbConnection.QuerySingleAsync<int>(sql, member);
                _dbConnection.Close();

                return Ok(new { success = true, message = "會員添加成功", memberId = newMemberId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "無法添加會員");
                return StatusCode(500, "內部服務器錯誤");
            }
        }


        [HttpGet("{memberId}")]
        public async Task<ActionResult> GetMember(int memberId)
        {
            try
            {
                string sql = @"
                    SELECT 
                        MemberId, MemberAccount, MemberName, MemberPhone, MemberEmail, MemberPublish, LastLogin, IP 
                    FROM 
                        [dbo].[Member] 
                    WHERE 
                        MemberId = @MemberId;";

                _dbConnection.Open();
                var member = await _dbConnection.QuerySingleOrDefaultAsync<MemberDto>(sql, new { MemberId = memberId });
                _dbConnection.Close();

                if (member == null)
                {
                    return NotFound("會員未找到");
                }

                return Ok(new { success = true, data = member });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "無法獲取會員詳情");
                return StatusCode(500, "內部服務器錯誤");
            }
        }


        [HttpPut("update")]
        public async Task<ActionResult> UpdateMember([FromBody] UpdateMemberDto member)
        {
            if (member == null || member.MemberId <= 0)
            {
                return BadRequest("會員數據無效");
            }

            try
            {
                string sql = @"
                    UPDATE [dbo].[Member]
                    SET 
                        MemberName = @MemberName,
                        MemberEmail = @MemberEmail,
                        MemberPhone = @MemberPhone,
                        MemberPassword = @MemberPassword
                    WHERE 
                        MemberId = @MemberId;";

                _dbConnection.Open();
                int rowsAffected = await _dbConnection.ExecuteAsync(sql, member);
                _dbConnection.Close();

                if (rowsAffected > 0)
                {
                    return Ok(new { success = true, message = "會員信息更新成功" });
                }
                else
                {
                    return NotFound("會員未找到");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "無法更新會員信息");
                return StatusCode(500, "內部服務器錯誤");
            }
        }


        [HttpDelete("delete/{memberId}")]
        public async Task<ActionResult> DeleteMember(int memberId)
        {
            try
            {
                string sql = @"
                    DELETE FROM [dbo].[Member] 
                    WHERE 
                        MemberId = @MemberId;";

                _dbConnection.Open();
                int rowsAffected = await _dbConnection.ExecuteAsync(sql, new { MemberId = memberId });
                _dbConnection.Close();

                if (rowsAffected > 0)
                {
                    return Ok(new { success = true, message = "會員刪除成功" });
                }
                else
                {
                    return NotFound("會員未找到");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "無法刪除會員");
                return StatusCode(500, "內部服務器錯誤");
            }
        }
    }
}
