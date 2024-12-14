using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagermentSystem.Request;
using PharmacyManagermentSystem.Services.MiniServiceReceiptDetail;

namespace PharmacyManagermentSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Employee")]
    public class ReceiptDetailController : ControllerBase
    {
        private readonly IReceiptDetailService _receiptDetailService;
        public ReceiptDetailController(IReceiptDetailService receiptDetailService)
        {
            _receiptDetailService = receiptDetailService;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _receiptDetailService.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPost("add")]
        public async Task<IActionResult> CreateReceiptDetail(CreateReceiptDetailRequest request)
        {
            try
            {
                var result = await _receiptDetailService.CreateReceiptDetail(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateReceiptDetail(UpdateReceiptDetailRequest request)
        {
            try
            {
                var result = await _receiptDetailService.UpdateReceiptDetail(request); return Ok(result);
            }

            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteReceiptDetail(DeleteReceiptDetailRequest request)
        {
            try
            {
                var result = await _receiptDetailService.DeleteReceiptDetail(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }

}
