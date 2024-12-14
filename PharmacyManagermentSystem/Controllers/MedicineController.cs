using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Services.MiniServiceMedicine;

namespace PharmacyManagermentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Employee")]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _medicineService;
        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }
        [HttpGet("getAll/{pageIndex}/{pageSize}/{pharmacyId}")]
        public async Task<IActionResult> GetAllMedicine(int pageIndex, int pageSize,int pharmacyId)
        {
            try
            {
                var result = await _medicineService.GetAll(pageIndex, pageSize,pharmacyId);
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
        [HttpDelete("delete/{id}/{batchnumber}/{categoryId}")]
        public async Task<IActionResult> DeleteMedicine(string id, string batchnumber, string categoryId)
        {
            try
            {
                var payload =new DeleteMedicineRequest
                {
                    Id = id,
                    BatchNumber = batchnumber,
                    CategoryId = categoryId
                };
                var result = await _medicineService.DeleteMedicine(payload);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("getByCategoryId/{CategoryId}/{pharmacyId}")]
        public async Task<IActionResult> GetByCategoryId(string CategoryId, int pharmacyId)
        {
            try
            {
                var result = await _medicineService.GetByCategoryId(CategoryId, pharmacyId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("getByBatchNumber/{BatchNumber}/{CategoryId}")]
        public async Task<IActionResult> GetByBatchNumber(string BatchNumber, string CategoryId)
        {
            try
            {
                var result = await _medicineService.GetByBatchNumber(BatchNumber, CategoryId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("getQuality/{BatchNumber}/{CategoryId}/{MedicineId}")]
        public async Task<IActionResult> GetQuanlity(string BatchNumber, string CategoryId,string MedicineId)
        {
            try
            {
                var result = await _medicineService.GetQuantity(BatchNumber, CategoryId,MedicineId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
