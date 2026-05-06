using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HeavenlyKingdom.Api.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
        public OrderController(IOrderService service) => _service = service;

        [HttpGet("active")]
        public async Task<IActionResult> GetActive()
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();
            return Ok(await _service.GetActiveAsync(userId.Value));
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();
            return Ok(await _service.GetHistoryAsync(userId.Value));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
        {
            var userId = GetUserId();
            if (userId == null) return Unauthorized();
            var created = await _service.CreateAsync(userId.Value, dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        private int? GetUserId()
        {
            var raw = HttpContext.Session.GetString("userId");
            return int.TryParse(raw, out var id) ? id : null;
        }
    }
}
