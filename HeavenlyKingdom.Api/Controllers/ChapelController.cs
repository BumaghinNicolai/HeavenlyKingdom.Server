using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

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
            var raw = HttpContext.Session.GetString("userId");
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

        // DELETE /api/chapel/candles/{id}
        [HttpDelete("candles/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _chapelService.DeleteAsync(id);
            if (!success) return NotFound(new { Message = "Candle not found" });
            return NoContent();
        }
    }
}
