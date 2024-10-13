using Microsoft.AspNetCore.Mvc;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Services.MiniServiceReturnSupplier;

namespace PharmacyManagermentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnSupplierController : ControllerBase
    {
        private readonly IReturnSupplierService _returnSupplierService;
        public ReturnSupplierController(IReturnSupplierService returnSupplierService)
        {
            _returnSupplierService = returnSupplierService;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _returnSupplierService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPost("add")]
        public async Task<IActionResult> CreateReturnSupplier([FromBody] CreateReturnSupplierRequest request)
        {
            try
            {
                var result = await _returnSupplierService.CreateReturnSupplier(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateReturnSupplier([FromBody] UpdateReturnSupplierRequest request)
        {
            try
            {
                var result = await _returnSupplierService.UpdateReturnSupplier(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteReturnSupplierRequest request)
        {
            try
            {
                var result = await _returnSupplierService.Delete(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}