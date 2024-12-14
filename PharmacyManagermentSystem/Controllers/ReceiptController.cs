using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Services.MiniServiceReceipt;

namespace PharmacyManagermentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Employee")]
    public class ReceiptController : ControllerBase
    {
        private readonly IReceiptService _receiptService;
        public ReceiptController(IReceiptService receiptService)
        {
            _receiptService = receiptService;
        }
        [HttpGet("getAll/{pageIndex}/{pageSize}/{pharmacyId}")]
        public async Task<IActionResult> GetAll(int pageIndex, int pageSize, int pharmacyId)
        {
            try
            {
                var response = await _receiptService.GetAll(pageIndex,pageSize,pharmacyId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var response = await _receiptService.getById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("add")]
        public async Task<IActionResult> CreateReceipt(CreateReceiptRequest request)
        {
            try
            {
                var response = await _receiptService.CreateReceipt(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPost("addReceipt")]
        public async Task<IActionResult> AddReceipt(ReceiptRequest request)
        {
            try
            {
                var response = await _receiptService.AddReceipt(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateReceipt(int id, UpdateReceiptRequest request)
        {
            try
            {
                var response = await _receiptService.UpdateReceipt(id, request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _receiptService.Delete(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

    }
}
