using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Services.MiniServiceUserShift;

namespace PharmacyManagermentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Employee")]
    public class UserShiftController : ControllerBase
    {
        private readonly IUserShiftService _userShiftService;
        public UserShiftController(IUserShiftService userShiftService)
        {
            _userShiftService = userShiftService;
        }
        [HttpGet("getAll/{from}/{to}/{pharmacyId}")]
        public async Task<IActionResult> GetAll(DateOnly from, DateOnly to, int pharmacyId)
        {
            try { 
                var userShifts = await _userShiftService.GetAll(from,to,pharmacyId);
                return Ok(userShifts);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var userShifts = await _userShiftService.GetById(id);
                return Ok(userShifts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("getByDate/{date}/{pharmacyId}")]
        public async Task<IActionResult> GetByDate(DateOnly date,int pharmacyId)
        {
            try
            {
                var userShifts = await _userShiftService.GetByDate(date,pharmacyId);
                return Ok(userShifts);
            }
            catch (Exception ex)
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
        [HttpDelete("delete/{id}/{date}/{pharmacyId}")]
        public async Task<IActionResult> Delete(string id, DateOnly date, int pharmacyId)
        {
            try
            {
                var request = new DeleteUserShiftRequest
                {
                    EmployeeId = id,
                    Date = date,
                    PharmacyId = pharmacyId
                };
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
