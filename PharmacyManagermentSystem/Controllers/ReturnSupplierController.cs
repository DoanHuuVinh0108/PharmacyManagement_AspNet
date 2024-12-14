using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Services.MiniServiceReturnSupplier;

namespace PharmacyManagermentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Employee")]
    public class ReturnSupplierController : ControllerBase
    {
        private readonly IReturnSupplierService _returnSupplierService;
        public ReturnSupplierController(IReturnSupplierService returnSupplierService)
        {
            _returnSupplierService = returnSupplierService;
        }
        [HttpGet("getAll/{pageIndex}/{pageSize}/{pharmacyId}")]
        public async Task<IActionResult> GetAll(int pageIndex, int pageSize, int pharmacyId)
        {
            try
            {
                var result = await _returnSupplierService.GetAll(pageIndex,pageSize,pharmacyId);
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

        [HttpDelete("delete/{id}/{batchNumber}/{categoryId}")]
        public async Task<IActionResult> Delete(string id, string batchNumber,string categoryId)
        {
            try
            {
                var request = new DeleteReturnSupplierRequest
                {
                    MedicineId = id,
                    BatchNumber = batchNumber,
                    CategoryId = categoryId
                };
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