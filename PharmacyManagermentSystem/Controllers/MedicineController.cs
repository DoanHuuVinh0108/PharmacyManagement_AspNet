using Microsoft.AspNetCore.Mvc;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Services.MiniServiceMedicine;

namespace PharmacyManagermentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _medicineService;
        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllMedicine()
        {
            try
            {
                var result = await _medicineService.GetAll();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            } 
        }
        [HttpPost("add")]
        public async Task<IActionResult> CreateMedicine([FromBody] CreateMedicineRequest payload)
        {
            try
            {
                var result = await _medicineService.CreateMedicine(payload);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateMedicine([FromBody] UpdateMedicineRequest payload)
        {
            try
            {
                var result = await _medicineService.UpdateMedicine( payload);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteMedicine([FromBody] DeleteMedicineRequest payload)
        {
            try
            {
                var result = await _medicineService.DeleteMedicine(payload);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
