using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Services.MiniServicePharmacy;

namespace PharmacyManagermentSystem.Controllerss
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PharmacyController : ControllerBase
    {
        private readonly IPharmacyService _pharmacyService;
        public PharmacyController(IPharmacyService pharmacyService)
        {
            _pharmacyService = pharmacyService;
        }
       
        [HttpGet("getAll/{pageIndex}/{pageSize}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllPharmacy(int pageIndex, int pageSize)
        {
            try
            {
                var response = await _pharmacyService.GetAllPharmacy(pageIndex, pageSize);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("get")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPharmacy()
        {
            try
            {
                var response = await _pharmacyService.GetPharmacy();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("add")]
       
        public async Task<IActionResult> CreatePharmacy([FromBody] CreatePharmacyRequest payload)
        {
            try
            {
                var response = await _pharmacyService.CreatePharmacy(payload);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdatePharmacy(int id, [FromBody] UpdatePharmacyRequest payload)
        {
            try
            {
                var response = await _pharmacyService.UpdatePharmacy(id, payload);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePharmacy(int id)
        {
            try
            {
                var response = await _pharmacyService.DeletePharmacy(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
