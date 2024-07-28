using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantManagement.API.Dtos;
using System;
using System.Data;
using System.Threading.Tasks;
using static RestaurantManagement.API.Dtos.CartDto;

namespace RestaurantManagement.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<CartController> _logger;

        public CartController(IDbConnection dbConnection, ILogger<CartController> logger)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{memberId}")]
        public async Task<ActionResult> Get(int memberId)
        {
            try
            {
                string sql = @"
                    SELECT 
                        c.CartId,
                        c.MemberId,
                        c.ProductId,
                        c.Quantity,
                        c.CreatedDate,
                        p.ProductName,
                        p.Price,
                        p.ProductImg1
                    FROM 
                        [dbo].[Cart] c
                    INNER JOIN
                        [dbo].[Product] p ON c.ProductId = p.ProductId
                    WHERE 
                        c.MemberId = @MemberId;";

                _dbConnection.Open();
                var cartItems = await _dbConnection.QueryAsync<CartItemDto>(sql, new { MemberId = memberId });
                _dbConnection.Close();

                return Ok(new { success = true, message = "獲取成功", data = cartItems });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "無法獲取購物車項目。");
                return StatusCode(500, "內部伺服器錯誤。");
            }
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddCartItem([FromBody] CartCreateDto cartDto)
        {
            if (cartDto == null || cartDto.MemberId <= 0 || cartDto.ProductId <= 0 || cartDto.Quantity <= 0)
            {
                return BadRequest("Invalid request data.");
            }

            try
            {
                _dbConnection.Open();

                // 檢查是否已經有相同的品項在購物車中
                string checkSql = @"
                    SELECT 
                        COUNT(*)
                    FROM 
                        [dbo].[Cart]
                    WHERE 
                        MemberId = @MemberId AND ProductId = @ProductId;";

                int count = await _dbConnection.ExecuteScalarAsync<int>(checkSql, new
                {
                    cartDto.MemberId,
                    cartDto.ProductId
                });

                if (count > 0)
                {
                    // 更新數量
                    string updateSql = @"
                        UPDATE [dbo].[Cart]
                        SET Quantity = Quantity + @Quantity
                        WHERE MemberId = @MemberId AND ProductId = @ProductId;";

                    await _dbConnection.ExecuteAsync(updateSql, new
                    {
                        cartDto.Quantity,
                        cartDto.MemberId,
                        cartDto.ProductId
                    });
                }
                else
                {
                    // 新增品項
                    string insertSql = @"
                        INSERT INTO [dbo].[Cart] (MemberId, ProductId, Quantity, CreatedDate)
                        VALUES (@MemberId, @ProductId, @Quantity, GETDATE());";

                    await _dbConnection.ExecuteAsync(insertSql, cartDto);
                }

                _dbConnection.Close();
                return Ok(new { success = true, message = "成功新增或更新購物車項目。" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "無法新增或更新購物車項目。");
                return StatusCode(500, new { success = false, message = "內部伺服器錯誤。" });
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateCartItem([FromBody] CartUpdateDto cartDto)
        {
            if (cartDto == null || cartDto.CartId <= 0 || cartDto.MemberId <= 0 || cartDto.ProductId <= 0 || cartDto.Quantity <= 0)
            {
                return BadRequest(new { success = false, message = "Invalid request data." });
            }

            try
            {
                string updateSql = @"
                    UPDATE [dbo].[Cart]
                    SET MemberId = @MemberId, ProductId = @ProductId, Quantity = @Quantity
                    WHERE CartId = @CartId;";

                _dbConnection.Open();
                int rowsAffected = await _dbConnection.ExecuteAsync(updateSql, cartDto);
                _dbConnection.Close();

                if (rowsAffected > 0)
                {
                    return Ok(new { success = true, message = $"成功更新購物車項目：{cartDto.CartId}" });
                }
                else
                {
                    return Ok(new { success = false, message = $"更新購物車項目失敗：{cartDto.CartId}" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "無法更新購物車項目。");
                return StatusCode(500, new { success = false, message = "內部伺服器錯誤。" });
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCartItem([FromBody] CartDeleteDto deleteDto)
        {
            try
            {
                string deleteSql = @"
                DELETE FROM [dbo].[Cart]
                WHERE CartId = @CartId;";

                _dbConnection.Open();
                int rowsAffected = await _dbConnection.ExecuteAsync(deleteSql, new { CartId = deleteDto.CartId });
                _dbConnection.Close();

                if (rowsAffected > 0)
                {
                    return Ok(new { success = true, message = $"成功刪除購物車項目：{deleteDto.CartId}" });
                }
                else
                {
                    return Ok(new { success = false, message = $"刪除購物車項目失敗：{deleteDto.CartId}" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "無法刪除購物車項目。");
                return StatusCode(500, new { success = false, message = "內部伺服器錯誤。" });
            }
        }
    }
}
