using Microsoft.AspNetCore.Mvc;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Services.MiniServiceRole;

namespace PharmacyManagermentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddRole([FromBody] CreateRoleRequest request)
        {
            try
            {
                var result = await _roleService.AddRole(request.RoleName);
                if (result.Succeeded)
                {
                    return Ok("Thêm Role thành công");
                }
                return BadRequest("Thêm Role thất bại");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            try
            {
                var result = await _roleService.DeleteRole(id);
                if (result.Succeeded)
                {
                    return Ok("Xóa Role thành công");
                }
                return BadRequest("Xóa Role thất bại");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                var roles = await _roleService.GetRoles();
                return Ok(roles);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleRequest updateRole)
        {
            try
            {
                var result = await _roleService.UpdateRole(updateRole.RoleId, updateRole.NewRoleName);
                if (result != null)
                {
                    return Ok("Cập nhật Role thành công");
                }
                return BadRequest("Cập nhật Role thất bại");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
