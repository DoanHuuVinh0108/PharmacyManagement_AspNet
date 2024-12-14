using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json.Linq;
using PharmacyManagermentSystem.Services.MiniServiceStatistics;

namespace PharmacyManagermentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Employee")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticService _statisticService;
        public StatisticsController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }
        [HttpGet("getProfitAllPharmacy")]
        public async Task<IActionResult> GetProfitByPharmacy(
            [FromQuery] int? Month,
            [FromQuery] int? Year,
            [FromQuery] DateOnly? fromDate,
            [FromQuery] DateOnly? toDate
        )
        {
            if (Year.HasValue)
            {
                try
                {
                    if (Month.HasValue)
                    {
                        var response1 = await _statisticService.GetProfitAllPharmacy(Month.Value, Year.Value);
                        return Ok(response1);
                    }
                    var response2 = await _statisticService.GetProfitAllPharmacy(Year.Value);
                    return Ok(response2);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"An error occurred: {ex.Message}");
                }
            }
            else if (fromDate.HasValue && toDate.HasValue)
            {
                try
                {
                    var response = await _statisticService.GetProfitAllPharmacy(fromDate.Value, toDate.Value);
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"An error occurred: {ex.Message}");
                }
            }
            else
            {
                return BadRequest("Invalid input: Provide either Month and Year, or formDate and toDate.");
            }

        }
        [HttpGet("getProfitWithPharmacy")]
        public async Task<IActionResult> GetProfitByPharmacy(
            [FromQuery] int? PharmacyId,
            [FromQuery] int? Month,
            [FromQuery] int? Year,
            [FromQuery] DateOnly? fromDate,
            [FromQuery] DateOnly? toDate
        )
        {
            if (!PharmacyId.HasValue)
            {
                return BadRequest("Invalid input: Provide PharmacyId.");
            }
            if (Year.HasValue)
            {
                try
                {
                    if (Month.HasValue)
                    {
                        var response1 = await _statisticService.GetProfitWithPharmacy(Month.Value, Year.Value, PharmacyId.Value);
                        return Ok(response1);
                    }
                    var response2 = await _statisticService.GetProfitWithPharmacy(Year.Value, PharmacyId.Value);
                    return Ok(response2);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"An error occurred: {ex.Message}");
                }
            }
            else if (fromDate.HasValue && toDate.HasValue)
            {
                try
                {
                    var response = await _statisticService.GetProfitWithPharmacy(fromDate.Value, toDate.Value, PharmacyId.Value);
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"An error occurred: {ex.Message}");
                }
            }
            else
            {
                return BadRequest("Invalid input: Provide either Month and Year, or formDate and toDate.");
            }

        }
        [HttpGet("getProfitProductAllPharmacy")]
        public async Task<IActionResult> GetProfitProductAllPharmacy(
            [FromQuery] int? Month,
            [FromQuery] int? Year,
            [FromQuery] string? CategoryId,
            [FromQuery] DateOnly? fromDate,
            [FromQuery] DateOnly? toDate
        )
        {
            if(string.IsNullOrEmpty(CategoryId))
            {
                return BadRequest("Invalid input: Provide CategoryId.");
            }
            if (Year.HasValue)
            {
                try
                {
                    if (Month.HasValue)
                    {
                        var response1 = await _statisticService.GetProfitProductAllPharmacy(Month.Value, Year.Value, CategoryId);
                        return Ok(response1);
                    }
                    var response2 = await _statisticService.GetProfitProductAllPharmacy(Year.Value, CategoryId);
                    return Ok(response2);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"An error occurred: {ex.Message}");
                }
            }
            else if (fromDate.HasValue && toDate.HasValue)
            {
                try
                {
                    var response = await _statisticService.GetProfitProductAllPharmacy(fromDate.Value, toDate.Value, CategoryId);
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"An error occurred: {ex.Message}");
                }
            }
            else
            {
                return BadRequest("Invalid input: Provide either Month and Year, or formDate and toDate.");
            }

        }

        [HttpGet("getProfitProductWithPharmacy")]
        public async Task<IActionResult> GetProfitProductByPharmacy(
            [FromQuery] int? PharmacyId,
            [FromQuery] int? Month,
            [FromQuery] int? Year,
            [FromQuery] string? CategoryId,
            [FromQuery] DateOnly? fromDate,
            [FromQuery] DateOnly? toDate
        )
        {
            if (!PharmacyId.HasValue)
            {
                return BadRequest("Invalid input: Provide PharmacyId.");
            }
            if(string.IsNullOrEmpty(CategoryId))
            {
                return BadRequest("Invalid input: Provide CategoryId.");
            }
            if (Year.HasValue)
            {
                try
                {
                    if (Month.HasValue)
                    {
                        var response1 = await _statisticService.GetProfitProductWithPharmacy(Month.Value, Year.Value, PharmacyId.Value, CategoryId);
                        return Ok(response1);
                    }
                    var response2 = await _statisticService.GetProfitProductWithPharmacy(Year.Value, PharmacyId.Value, CategoryId);
                    return Ok(response2);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"An error occurred: {ex.Message}");
                }
            }
            else if (fromDate.HasValue && toDate.HasValue)
            {
                try
                {
                    var response = await _statisticService.GetProfitWithProductPharmacy(fromDate.Value, toDate.Value, PharmacyId.Value, CategoryId);
                    return Ok(response);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"An error occurred: {ex.Message}");
                }
            }
            else
            {
                return BadRequest("Invalid input: Provide either Month and Year, or formDate and toDate.");
            }


        }
        [HttpGet("getTopSellingProduct")]
        public async Task<IActionResult> GetTopSellingProduct(
            [FromQuery] int? Month,
            [FromQuery] int? Year
        )
        {
            try
            {
                if(Month.HasValue && Year.HasValue)
                {
                    var response = await _statisticService.GetTopSellByAllPharmacy(Month.Value, Year.Value);
                    return Ok(response);
                }
                return BadRequest("Invalid input: Provide Month and Year.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpGet("getTopSellingProductByPharmacy")]
        public async Task<IActionResult> GetTopSellingProductByPharmacy(
            [FromQuery] int? Month,
            [FromQuery] int? Year,
            [FromQuery] int? PharmacyId
        )
        {
            if (!PharmacyId.HasValue)
            {
                return BadRequest("Invalid input: Provide PharmacyId.");
            }
            try
            {
                if(Month.HasValue && Year.HasValue)
                {
                    var response = await _statisticService.GetTopSellByPharmacy(Month.Value, Year.Value, PharmacyId.Value);
                    return Ok(response);
                }
                return BadRequest("Invalid input: Provide Month and Year.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
