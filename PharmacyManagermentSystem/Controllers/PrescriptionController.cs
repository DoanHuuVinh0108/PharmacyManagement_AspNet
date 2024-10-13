using Microsoft.AspNetCore.Mvc;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Services.MiniServicePrescription;
namespace PharmacyManagermentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpPost("add")]
        public async Task<IActionResult> CreatePrescription([FromForm] CreatePrescriptionRequest request)
        {
            try
            {
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
