using Microsoft.AspNetCore.Mvc;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Services.MiniServiceOrder;

namespace PharmacyManagermentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var response = await _orderService.GetAll();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("add")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            try
            {
                var response = await _orderService.CreateOrder(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateOrder(int id,[FromBody] UpdateOrderRequest request)
        {
            try
            {
                var response = await _orderService.UpdateOrder(id,request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                var response = await _orderService.DeleteOrder(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
