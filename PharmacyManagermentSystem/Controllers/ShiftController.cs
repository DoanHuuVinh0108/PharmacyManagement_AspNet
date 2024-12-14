using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Services.MiniServiceShift;

namespace PharmacyManagermentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Employee")]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftService _shiftService;
        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }
        [HttpGet("getAll/{from}/{to}/{pharmacyId}")]
        public async Task<IActionResult> GetAll(DateOnly from, DateOnly to, int pharmacyId)
        {
            try
            {
                var response = await _shiftService.GetAll(from,to,pharmacyId);
                return Ok(response);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("add")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> CreateShift([FromBody] CreateShiftRequest request)
        {
            try
            {
                var response = await _shiftService.CreateShift(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("update")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> UpdateShift([FromBody] UpdateShiftRequest request)
        {
            try
            {
                var response = await _shiftService.UpdateShift(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("delete")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteShift([FromBody] DeleteShiftRequest request)
        {
            try
            {
                var response = await _shiftService.DeleteShift(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("getByPage/{pageIndex}/{pageSize}/{pharmacyId}")]
        public async Task<IActionResult> GetByPage(int pageIndex, int pageSize, int pharmacyId)
        {
            try
            {
                var response = await _shiftService.getByPage(pageIndex, pageSize, pharmacyId);
                return Ok(response);
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
