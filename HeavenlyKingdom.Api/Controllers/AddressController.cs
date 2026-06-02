using HeavenlyKingdom.Api.Filters;
using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HeavenlyKingdom.Api.Controllers
{
    [Route("api/addresses")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService) => _addressService = addressService;

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
            var addresses = await _addressService.GetByUserIdAsync(userId);
            return Ok(addresses);
        }

        [HttpPost]
        [UserMod]
        public async Task<IActionResult> Create([FromBody] CreateAddressDto dto)
        {
            var userId = GetUserId()!.Value;
            var result = await _addressService.CreateAsync(userId, dto);
            return Created($"/api/addresses/{result.Id}", result);
        }

        [HttpPut("{id}")]
        [UserMod]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAddressDto dto)
        {
            var userId = GetUserId()!.Value;
            var result = await _addressService.UpdateAsync(id, userId, dto);
            if (result == null) return NotFound(new { Message = "Address not found" });
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [UserMod]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId()!.Value;
            var success = await _addressService.DeleteAsync(id, userId);
            if (!success) return NotFound(new { Message = "Address not found" });
            return NoContent();
        }

        [HttpPut("{id}/set-default")]
        [UserMod]
        public async Task<IActionResult> SetDefault(int id)
        {
            var userId = GetUserId()!.Value;
            var success = await _addressService.SetDefaultAsync(id, userId);
            if (!success) return NotFound(new { Message = "Address not found" });
            return Ok(new { Message = "Default address updated" });
        }
    }
}
