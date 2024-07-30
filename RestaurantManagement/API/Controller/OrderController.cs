using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantManagement.API.Dtos;
using RestaurantManagement.Models;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static RestaurantManagement.API.Dtos.CartDto;

namespace RestaurantManagement.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IDbConnection dbConnection, ILogger<OrderController> logger)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{memberId}")]
        public async Task<ActionResult> GetList(int memberId)
        {
            try
            {
                string sql = @"
                    SELECT 
                        OrderId,
                        OrderDate,
                        MemberId,
                        TotalAmount
                    FROM 
                        [dbo].[Order] 
                    WHERE 
                        MemberId = @MemberId;";

                _dbConnection.Open();
                var orders = await _dbConnection.QueryAsync<OrderDto>(sql, new { MemberId = memberId });
                _dbConnection.Close();

                return Ok(new { success = true, message = "獲取成功", data = orders });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "無法獲取訂單。");
                return StatusCode(500, "內部伺服器錯誤。");
            }
        }

        [HttpGet("OrderDetails/{orderId}")]
        public async Task<ActionResult> OrderDetails(int orderId)
        {
            try
            {
                string sql = @"
                    SELECT
                        [OrderDetailsId],
                        [OrderId],
                        [ProductId],
                        [Quantity],
                        [UnitPrice]
                    FROM [dbo].[OrderDetails]
                    WHERE 
                        OrderId = @OrderId;";

                _dbConnection.Open();
                var orderDetails = await _dbConnection.QueryAsync<OrderDetailDto>(sql, new { OrderId = orderId });
                _dbConnection.Close();

                return Ok(new { success = true, message = "獲取成功", data = orderDetails });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "無法獲取訂單詳情。");
                return StatusCode(500, "內部伺服器錯誤。");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder([FromBody] OrderCreateDto orderDto)
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }

            using var transaction = _dbConnection.BeginTransaction();

            try
            {
                // 1. 從 Cart 表中根據 memberId 搜尋購物車項目
                string cartSql = @"
            SELECT 
                c.CartId,
                c.ProductId,
                c.Quantity,
                p.Price
            FROM 
                [dbo].[Cart] c
            INNER JOIN 
                [dbo].[Product] p ON c.ProductId = p.ProductId
            WHERE 
                c.MemberId = @MemberId;";

                var cartItems = (await _dbConnection.QueryAsync<CartItemDto>(cartSql, new { MemberId = orderDto.MemberId }, transaction)).ToList();

                if (cartItems.Count == 0)
                {
                    return BadRequest(new { success = false, message = "購物車為空。" });
                }

                // 2. 計算訂單總金額
                decimal totalAmount = cartItems.Sum(item => item.Quantity * item.Price);

                // 3. 創建訂單
                string insertOrderSql = @"
            INSERT INTO [dbo].[Order] (OrderDate, MemberId, TotalAmount)
            VALUES (@OrderDate, @MemberId, @TotalAmount);
            SELECT CAST(SCOPE_IDENTITY() as int);";

                var orderId = await _dbConnection.QuerySingleAsync<int>(insertOrderSql, new
                {
                    OrderDate = DateTime.Now,
                    MemberId = orderDto.MemberId,
                    TotalAmount = totalAmount
                }, transaction);

                // 4. 插入訂單明細
                string insertOrderDetailSql = @"
            INSERT INTO [dbo].[OrderDetails] (OrderId, ProductId, Quantity, UnitPrice)
            VALUES (@OrderId, @ProductId, @Quantity, @UnitPrice);";

                foreach (var item in cartItems)
                {
                    await _dbConnection.ExecuteAsync(insertOrderDetailSql, new
                    {
                        OrderId = orderId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.Price
                    }, transaction);
                }

                // 5. 從購物車中刪除已創建訂單的項目
                string deleteCartSql = @"
            DELETE FROM [dbo].[Cart]
            WHERE MemberId = @MemberId;";

                await _dbConnection.ExecuteAsync(deleteCartSql, new { MemberId = orderDto.MemberId }, transaction);

                transaction.Commit();
                _dbConnection.Close();

                return Ok(new { success = true, message = "訂單已成功創建。" });
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError(ex, "無法創建訂單。");
                return StatusCode(500, new { success = false, message = "內部伺服器錯誤。" });
            }

            
        }


       

        [HttpDelete("{orderId}")]
        public async Task<ActionResult> DeleteOrder(int orderId)
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }

            using var transaction = _dbConnection.BeginTransaction();

            try
            {
                string deleteOrderDetailSql = @"
                    DELETE FROM [dbo].[OrderDetails]
                    WHERE OrderId = @OrderId;";

                await _dbConnection.ExecuteAsync(deleteOrderDetailSql, new { OrderId = orderId }, transaction);

                string deleteOrderSql = @"
                    DELETE FROM [dbo].[Order]
                    WHERE OrderId = @OrderId;";

                await _dbConnection.ExecuteAsync(deleteOrderSql, new { OrderId = orderId }, transaction);

                transaction.Commit();
                _dbConnection.Close();

                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                _logger.LogError(ex, "無法刪除訂單。");
                return StatusCode(500, "內部伺服器錯誤。");
            }
        }
    }
}
