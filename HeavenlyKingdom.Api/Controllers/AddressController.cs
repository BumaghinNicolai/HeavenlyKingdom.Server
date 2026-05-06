using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

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
            var raw = HttpContext.Session.GetString("userId");
            return int.TryParse(raw, out var id) ? id : null;
        }

        // GET /api/addresses
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized(new { Message = "Not logged in" });
            var addresses = await _addressService.GetByUserIdAsync(userId.Value);
            return Ok(addresses);
        }

        // POST /api/addresses
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAddressDto dto)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized(new { Message = "Not logged in" });
            var result = await _addressService.CreateAsync(userId.Value, dto);
            return Created($"/api/addresses/{result.Id}", result);
        }

        // PUT /api/addresses/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAddressDto dto)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized(new { Message = "Not logged in" });
            var result = await _addressService.UpdateAsync(id, userId.Value, dto);
            if (result == null) return NotFound(new { Message = "Address not found" });
            return Ok(result);
        }

        // DELETE /api/addresses/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized(new { Message = "Not logged in" });
            var success = await _addressService.DeleteAsync(id, userId.Value);
            if (!success) return NotFound(new { Message = "Address not found" });
            return NoContent();
        }

        // PUT /api/addresses/{id}/set-default
        [HttpPut("{id}/set-default")]
        public async Task<IActionResult> SetDefault(int id)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized(new { Message = "Not logged in" });
            var success = await _addressService.SetDefaultAsync(id, userId.Value);
            if (!success) return NotFound(new { Message = "Address not found" });
            return Ok(new { Message = "Default address updated" });
        }
    }
}
