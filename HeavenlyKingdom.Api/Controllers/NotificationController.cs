using HeavenlyKingdom.Api.Filters;
using HeavenlyKingdom.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            var raw = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(raw, out var id) ? id : null;
        }

        [HttpGet]
        [UserMod]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId()!.Value;
            var result = await _notificationService.GetByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpPut("{id}/read")]
        [UserMod]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            await _notificationService.MarkAsReadAsync(id);
            return Ok(new { Message = "Notification marked as read" });
        }

        [HttpPut("mark-all-read")]
        [UserMod]
        public async Task<IActionResult> MarkAllAsRead()
        {
            var userId = GetUserId()!.Value;
            await _notificationService.MarkAllAsReadAsync(userId);
            return Ok(new { Message = "All notifications marked as read" });
        }
    }
}
