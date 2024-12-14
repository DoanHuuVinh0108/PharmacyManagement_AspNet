using Microsoft.AspNetCore.Mvc;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Services.MiniServiceSalary;
using Microsoft.AspNetCore.Authorization;
using PharmacyManagermentSystem.Model;

namespace PharmacyManagermentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles="Admin")]
    public class SalaryController : ControllerBase
    {
        private readonly ISalaryService _salaryService;
        public SalaryController(ISalaryService salaryService)
        {
            _salaryService = salaryService;
        }
        [HttpGet("getAll/{pageIndex}/{pageSize}/{pharmacyId}")]
        public async Task<IActionResult> GetAllSalary(int pageIndex, int pageSize, int pharmacyId)
        {
            try
            {
                var result = await _salaryService.GetAllSalary(pageIndex,pageSize,pharmacyId);
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
        [HttpDelete("delete/{month}/{year}/{employeeId}")]
        public async Task<IActionResult> DeleteSalary(int month, int year, string employeeId)
        {
            try
            {
               
                var result = await _salaryService.DeleteSalary(month,year,employeeId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
