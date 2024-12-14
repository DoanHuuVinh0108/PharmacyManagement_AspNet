using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Services.MiniServiceDestructiveMedicine;

namespace PharmacyManagermentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Employee")]
    public class DestructiveMedicineController : ControllerBase
    {
        private readonly IDestructiveMedicineService _destructiveMedicineService;
        public DestructiveMedicineController(IDestructiveMedicineService destructiveMedicineService)
        {
            _destructiveMedicineService = destructiveMedicineService;
        }
        [HttpGet("getAll/{pageIndex}/{pageSize}/{pharmacyId}")]
        public async Task<IActionResult> GetAll(int pageIndex, int pageSize,int pharmacyId)
        {
            try
            {
                var result = await _destructiveMedicineService.GetAll(pageIndex,pageSize,pharmacyId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPost("add")]
        public async Task<IActionResult> CreateDestructiveMedicine([FromBody] CreateDestructiveMedicineRequest request)
        {
            try
            {
                var result = await _destructiveMedicineService.CreateDestructiveMedicine(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateDestructiveMedicine([FromBody] UpdateDestructiveMedicineRequest request)
        {
            try
            {
                var result = await _destructiveMedicineService.UpdateDestructiveMedicine(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("delete/{id}/{batchNumber}/{CategoryId}")]
        public async Task<IActionResult> DeleteDestructiveMedicine(string id, string batchNumber, string CategoryId)
        {
            try
            {
                var request = new DeleteDestructiveMedicineRequest
                {
                    MedicineId = id,
                    BatchNumber = batchNumber,
                    CategoryId = CategoryId
                };
                var result = await _destructiveMedicineService.DeleteDestructiveMedicine(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
