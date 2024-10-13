using Microsoft.AspNetCore.Mvc;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Services.MiniServiceDestructiveMedicine;

namespace PharmacyManagermentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestructiveMedicineController : ControllerBase
    {
        private readonly IDestructiveMedicineService _destructiveMedicineService;
        public DestructiveMedicineController(IDestructiveMedicineService destructiveMedicineService)
        {
            _destructiveMedicineService = destructiveMedicineService;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _destructiveMedicineService.GetAll();
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
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteDestructiveMedicine([FromBody] DeleteDestructiveMedicineRequest request)
        {
            try
            {
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
