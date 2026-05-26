using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HeavenlyKingdom.Api.Controllers
{
    [ApiController]
    [Route("api/donations")]
    public class PublicDonationController : ControllerBase
    {
        private readonly IDonationService _service;
        public PublicDonationController(IDonationService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetGoal()
        {
            var result = await _service.GetGoalAsync();
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Donate([FromBody] AddProgressDto dto)
        {
            if (dto.Amount <= 0) return BadRequest(new { Message = "Amount must be positive" });
            var result = await _service.AddProgressAsync(dto);
            return Ok(result);
        }
    }
}
