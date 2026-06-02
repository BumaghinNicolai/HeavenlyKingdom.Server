using HeavenlyKingdom.Api.Filters;
using HeavenlyKingdom.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
            var raw = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(raw, out var id) ? id : null;
        }

        [HttpGet]
        [UserMod]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId()!.Value;
            var result = await _favoriteService.GetByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpPost("{productId}")]
        [UserMod]
        public async Task<IActionResult> Add(int productId)
        {
            var userId = GetUserId()!.Value;
            var result = await _favoriteService.AddAsync(userId, productId);
            if (result == null) return Conflict(new { Message = "Already in favorites" });
            return Created($"/api/favorites", result);
        }

        [HttpDelete("{productId}")]
        [UserMod]
        public async Task<IActionResult> Delete(int productId)
        {
            var userId = GetUserId()!.Value;
            var success = await _favoriteService.DeleteAsync(userId, productId);
            if (!success) return NotFound(new { Message = "Not found in favorites" });
            return NoContent();
        }
    }
}
