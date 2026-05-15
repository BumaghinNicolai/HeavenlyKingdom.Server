using HeavenlyKingdom.Api.Filters;
using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HeavenlyKingdom.Api.Controllers
{
    [Route("api/admin/donations")]
    [ApiController]
    public class DonationController : ControllerBase
    {
        private readonly IDonationService _donationService;
        public DonationController(IDonationService donationService) =>
            _donationService = donationService;

        // GET /api/admin/donations
        [HttpGet]
        [AdminMod]
        public async Task<IActionResult> GetGoal()
        {
            var result = await _donationService.GetGoalAsync();
            if (result == null) return NotFound(new { Message = "Donation goal not set" });
            return Ok(result);
        }

        // PUT /api/admin/donations
        [HttpPut]
        [AdminMod]
        public async Task<IActionResult> UpdateGoal([FromBody] UpdateDonationGoalDto dto)
        {
            var result = await _donationService.UpdateGoalAsync(dto);
            return Ok(result);
        }

        // POST /api/admin/donations/progress
        [HttpPost("progress")]
        [AdminMod]
        public async Task<IActionResult> AddProgress([FromBody] AddProgressDto dto)
        {
            var result = await _donationService.AddProgressAsync(dto);
            return Ok(result);
        }

        // POST /api/admin/donations/reset
        [HttpPost("reset")]
        [AdminMod]
        public async Task<IActionResult> Reset()
        {
            var result = await _donationService.ResetAsync();
            return Ok(result);
        }
    }
}
