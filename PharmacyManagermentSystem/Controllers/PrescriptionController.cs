using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Services.MiniServicePrescription;
namespace PharmacyManagermentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Employee")]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;
        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _prescriptionService.GetAll();
                return Ok(result);
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await _prescriptionService.GetById(id);
                return Ok(result);
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("getByPage/{pageIndex}/{pageSize}")]
        public async Task<IActionResult> GetByPage(int pageIndex, int pageSize)
        {
            try
            {
                var result = await _prescriptionService.GetByPage(pageIndex,pageSize);
                return Ok(result);
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("add")]
        public async Task<IActionResult> CreatePrescription([FromForm] string id,
        [FromForm] string customerId,
        [FromForm] int doctorId,
        [FromForm] string medicines,
        IFormFile file)
        {
            try
            {
                var request = new CreatePrescriptionRequest
                {
                    Id = id,
                    CustomerId = customerId,
                    DoctorId = doctorId,
                    File = file,
                    Medicines = JsonConvert.DeserializeObject<List<CreatePrescribeMedicineRequest>>(medicines)
                };
                var result = await _prescriptionService.CreatePrescription(request);
                return Ok(result);
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdatePrescription(string id,[FromForm] UpdatePrescriptionRequest request)
        {
            try
            {
                var result = await _prescriptionService.UpdatePrescription(request,id);
                return Ok(result);
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePrescription(string id)
        {
            try
            {
                var result = await _prescriptionService.DeletePrescription(id);
                return Ok(result);
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
