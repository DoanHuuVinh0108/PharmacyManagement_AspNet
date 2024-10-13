using Microsoft.AspNetCore.Mvc;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Services.MiniServiceSalary;

namespace PharmacyManagermentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryService _salaryService;
        public SalaryController(ISalaryService salaryService)
        {
            _salaryService = salaryService;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllSalary()
        {
            try
            {
                var result = await _salaryService.GetAllSalary();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            } 
        }
        [HttpPost("add")]
        public async Task<IActionResult> CreateSalary([FromBody] CreateSalaryRequest payload)
        {
            try
            {
                var result = await _salaryService.CreateSalary(payload);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateSalary([FromBody] UpdateSalaryRequest payload)
        {
            try
            {
                var result = await _salaryService.UpdateSalary( payload);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteSalary([FromBody] DeleteSalaryRequest payload)
        {
            try
            {
                var result = await _salaryService.DeleteSalary(payload);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
