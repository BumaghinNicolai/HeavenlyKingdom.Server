using HeavenlyKingdom.Api.Filters;
using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HeavenlyKingdom.Api.Controllers
{
    [Route("api/indulgences")]
    [ApiController]
    public class IndulgenceController : ControllerBase
    {
        private readonly IIndulgenceService _indulgenceService;
        public IndulgenceController(IIndulgenceService indulgenceService) =>
            _indulgenceService = indulgenceService;

        private int? GetUserId()
        {
            var raw = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(raw, out var id) ? id : null;
        }

        // GET /api/indulgences
        [HttpGet]
        [UserMod]
        public async Task<IActionResult> GetHistory()
        {
            var result = await _indulgenceService.GetHistoryAsync(GetUserId()!.Value);
            return Ok(result);
        }

        // POST /api/indulgences
        [HttpPost]
        public async Task<IActionResult> Purchase([FromBody] PurchaseIndulgenceDto dto)
        {
            var userId = GetUserId(); // nullable — гости тоже могут купить
            var result = await _indulgenceService.PurchaseAsync(userId, dto);
            return Created("/api/indulgences", result);
        }
    }
}
