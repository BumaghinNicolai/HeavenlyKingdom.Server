using HeavenlyKingdom.Api.Filters;
using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HeavenlyKingdom.Api.Controllers
{
    [Route("api/chapel")]
    [ApiController]
    public class ChapelController : ControllerBase
    {
        private readonly IChapelService _chapelService;
        public ChapelController(IChapelService chapelService) =>
            _chapelService = chapelService;

        private int? GetUserId()
        {
            var raw = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(raw, out var id) ? id : null;
        }

        // GET /api/chapel/candles
        [HttpGet("candles")]
        public async Task<IActionResult> GetActive()
        {
            var result = await _chapelService.GetActiveAsync();
            return Ok(result);
        }

        // POST /api/chapel/place
        [HttpPost("place")]
        public async Task<IActionResult> Place([FromBody] PlaceCandleDto dto)
        {
            var userId = GetUserId(); // может быть null — гости тоже могут ставить
            var result = await _chapelService.PlaceCandleAsync(userId, dto);
            if (result == null) return Conflict(new { Message = "Slot is already taken" });
            return Created("/api/chapel/candles", result);
        }

        // GET /api/chapel/my-candles
        [HttpGet("my-candles")]
        [UserMod]
        public async Task<IActionResult> GetMy()
        {
            var userId = GetUserId()!.Value;
            var result = await _chapelService.GetMyAsync(userId);
            return Ok(result);
        }

        // DELETE /api/chapel/candles/{id}
        [HttpDelete("candles/{id}")]
        [UserMod]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            var isAdmin = HttpContext.User.FindFirstValue(ClaimTypes.Role) == "2";
            var result = await _chapelService.DeleteAsync(id, userId, isAdmin);
            return result switch
            {
                null  => StatusCode(403, new { Message = "You don't have permission to delete this candle" }),
                false => NotFound(new { Message = "Candle not found" }),
                _     => NoContent()
            };
        }
    }
}
