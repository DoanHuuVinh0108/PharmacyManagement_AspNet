using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmacyManagermentSystem.Services.MiniServiceNotification;

namespace PharmacyManagermentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        [HttpGet("get/{userId}")]
        public async Task<IActionResult> GetNotificationsAsync(string userId)
        {
            try
            {
                var result = await _notificationService.GetNotificationsAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpGet("getAll/{userId}")]
        public async Task<IActionResult> GetAllNotificationsAsync(string userId)
        {
            try
            {
                var result = await _notificationService.GetAllNotificationsAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
        [HttpPost("read/{id}")]
        public async Task<IActionResult> Read(int id)
        {
            try
            {
                var result = await _notificationService.Read(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
