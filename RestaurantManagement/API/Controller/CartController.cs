using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

                return Ok(cartItems);
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
                string insertSql = @"
                    INSERT INTO [dbo].[Cart] (MemberId, ProductId, Quantity, CreatedDate)
                    VALUES (@MemberId, @ProductId, @Quantity, GETDATE());";

                _dbConnection.Open();
                await _dbConnection.ExecuteAsync(insertSql, cartDto);
                _dbConnection.Close();

                return Ok("成功新增購物車項目。");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "無法新增購物車項目。");
                return StatusCode(500, "內部伺服器錯誤。");
            }
        }


        [HttpPut("update")]
        public async Task<ActionResult> UpdateCartItem([FromBody] CartUpdateDto cartDto)
        {
            if (cartDto == null || cartDto.CartId <= 0 || cartDto.MemberId <= 0 || cartDto.ProductId <= 0 || cartDto.Quantity <= 0)
            {
                return BadRequest("Invalid request data.");
            }

            try
            {
                string updateSql = @"
                    UPDATE [dbo].[Cart]
                    SET MemberId = @MemberId, ProductId = @ProductId, Quantity = @Quantity
                    WHERE CartId = @CartId;";

                _dbConnection.Open();
                await _dbConnection.ExecuteAsync(updateSql, cartDto);
                _dbConnection.Close();

                return Ok($"成功更新購物車項目：{cartDto.CartId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "無法更新購物車項目。");
                return StatusCode(500, "內部伺服器錯誤。");
            }
        }


        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteCartItem([FromBody] CartDeleteDto deleteDto)
        {
            if (deleteDto == null || deleteDto.CartId <= 0)
            {
                return BadRequest("Invalid delete request.");
            }

            try
            {
                string deleteSql = @"
                    DELETE FROM [dbo].[Cart]
                    WHERE CartId = @CartId;";

                _dbConnection.Open();
                await _dbConnection.ExecuteAsync(deleteSql, new { CartId = deleteDto.CartId });
                _dbConnection.Close();

                return Ok($"成功刪除購物車項目：{deleteDto.CartId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "無法刪除購物車項目：{CartId}", deleteDto.CartId);
                return StatusCode(500, "內部伺服器錯誤。");
            }
        }
    }
}
