using Microsoft.AspNetCore.Mvc;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Services.MiniServiceUserShift;

namespace PharmacyManagermentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserShiftController : ControllerBase
    {
        private readonly IUserShiftService _userShiftService;
        public UserShiftController(IUserShiftService userShiftService)
        {
            _userShiftService = userShiftService;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try { 
                var userShifts = await _userShiftService.GetAll();
                return Ok(userShifts);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] CreateUserShiftRequest request)
        {
            try
            {
                var response = await _userShiftService.CreateUserShift(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserShiftRequest request)
        {
            try
            {
                var response = await _userShiftService.UpdateUserShift(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteUserShiftRequest request)
        {
            try
            {
                var response = await _userShiftService.DeleteUserShift(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
