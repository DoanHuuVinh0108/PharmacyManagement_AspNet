
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Services.MiniServiceUser;

namespace PharmacyManagermentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Employee")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("getAll/{pageIndex}/{pageSize}")]
        public async Task<IActionResult> GetUsers(int pageIndex, int pageSize)
        {
            try
            {
                var response = await _userService.GetUsers(pageIndex,pageSize);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }

        }
        [HttpPost("add")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            try
            {
                var response = await _userService.CreateUserAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }

        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        {
            try
            {
                var response = await _userService.UpdateUserAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }

        }
        [HttpDelete("delete/{id}")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteUser( string id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }

        }
        [HttpGet("findByPhoneNumber/{phoneNumber}")]
        public async Task<IActionResult> FindByPhoneNumber(string phoneNumber)
        {
            try
            {
                var response = await _userService.FindByPhoneNumber(phoneNumber);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }

        }
        [HttpGet("getByRole/{role}")]
        public async Task<IActionResult> GetByRole(string role)
        {
            try
            {
                var response = await _userService.GetByRole(role);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);

            }

        }
    }
}
