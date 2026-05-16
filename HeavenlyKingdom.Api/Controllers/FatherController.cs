using HeavenlyKingdom.Api.Filters;
using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HeavenlyKingdom.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FatherController : ControllerBase
    {
        private readonly IFatherService _service;
        public FatherController(IFatherService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("me")]
        [FatherMod]
        public async Task<IActionResult> GetMe()
        {
            var userIdStr = HttpContext.Session.GetString("userId");
            if (!int.TryParse(userIdStr, out var userId)) return Unauthorized();
            var result = await _service.GetByUserIdAsync(userId);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPut("me")]
        [FatherMod]
        public async Task<IActionResult> UpdateMe([FromBody] UpdateFatherProfileDto dto)
        {
            var userIdStr = HttpContext.Session.GetString("userId");
            if (!int.TryParse(userIdStr, out var userId)) return Unauthorized();
            var result = await _service.UpdateProfileAsync(userId, dto);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpGet("orders")]
        [FatherMod]
        public async Task<IActionResult> GetOrders() =>
            Ok(await _service.GetAllOrdersAsync());

        [HttpPost]
        [AdminMod]
        public async Task<IActionResult> Create(CreateFatherDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [AdminMod]
        public async Task<IActionResult> Update(int id, UpdateFatherDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpDelete("{id}")]
        [AdminMod]
        public async Task<IActionResult> Delete(int id) =>
            await _service.DeleteAsync(id) ? NoContent() : NotFound();
    }
}
