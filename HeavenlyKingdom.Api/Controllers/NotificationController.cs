using HeavenlyKingdom.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HeavenlyKingdom.Api.Controllers
{
    [Route("api/notifications")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        public NotificationController(INotificationService notificationService) =>
            _notificationService = notificationService;

        private int? GetUserId()
        {
            var raw = HttpContext.Session.GetString("userId");
            return int.TryParse(raw, out var id) ? id : null;
        }

        // GET /api/notifications
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized(new { Message = "Not logged in" });
            var result = await _notificationService.GetByUserIdAsync(userId.Value);
            return Ok(result);
        }

        // PUT /api/notifications/{id}/read
        [HttpPut("{id}/read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized(new { Message = "Not logged in" });
            await _notificationService.MarkAsReadAsync(id);
            return Ok(new { Message = "Notification marked as read" });
        }

        // PUT /api/notifications/mark-all-read
        [HttpPut("mark-all-read")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized(new { Message = "Not logged in" });
            await _notificationService.MarkAllAsReadAsync(userId.Value);
            return Ok(new { Message = "All notifications marked as read" });
        }
    }
}
