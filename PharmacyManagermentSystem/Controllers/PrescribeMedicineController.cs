using Microsoft.AspNetCore.Mvc;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Services.MiniServicePrescribeMedicine;

namespace PharmacyManagermentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescribeMedicineController : ControllerBase
    {
        private readonly IPrescribeMedicineService _prescribeMedicineService;
        public PrescribeMedicineController(IPrescribeMedicineService prescribeMedicineService)
        {
            _prescribeMedicineService = prescribeMedicineService;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _prescribeMedicineService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("add")]
        public async Task<IActionResult> CreatePrescribeMedicine([FromBody] CreatePrescribeMedicineRequest request)
        {
            try
            {
                var result = await _prescribeMedicineService.CreatePrescribeMedicine(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdatePrescribeMedicine([FromBody] UpdatePrescribeMedicineRequest request)
        {
            try
            {
                var result = await _prescribeMedicineService.UpdatePrescribeMedicine(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeletePrescribeMedicine([FromBody] DeletePrescribeMedicineRequest request)
        {
            try
            {
                var result = await _prescribeMedicineService.DeletePrescribeMedicine(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
