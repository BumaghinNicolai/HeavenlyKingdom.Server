using HeavenlyKingdom.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HeavenlyKingdom.Api.Controllers
{
    [Route("api/favorites")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;
        public FavoriteController(IFavoriteService favoriteService) =>
            _favoriteService = favoriteService;

        private int? GetUserId()
        {
            var raw = HttpContext.Session.GetString("userId");
            return int.TryParse(raw, out var id) ? id : null;
        }

        // GET /api/favorites
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized(new { Message = "Not logged in" });
            var result = await _favoriteService.GetByUserIdAsync(userId.Value);
            return Ok(result);
        }

        // POST /api/favorites/{productId}
        [HttpPost("{productId}")]
        public async Task<IActionResult> Add(int productId)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized(new { Message = "Not logged in" });
            var result = await _favoriteService.AddAsync(userId.Value, productId);
            if (result == null) return Conflict(new { Message = "Already in favorites" });
            return Created($"/api/favorites", result);
        }

        // DELETE /api/favorites/{productId}
        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized(new { Message = "Not logged in" });
            var success = await _favoriteService.DeleteAsync(userId.Value, productId);
            if (!success) return NotFound(new { Message = "Not found in favorites" });
            return NoContent();
        }
    }
}
