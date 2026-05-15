using HeavenlyKingdom.Api.Filters;
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
        [UserMod]
        public async Task<IActionResult> GetActive()
        {
            var userId = GetUserId()!.Value;
            return Ok(await _service.GetActiveAsync(userId));
        }

        [HttpGet("history")]
        [UserMod]
        public async Task<IActionResult> GetHistory()
        {
            var userId = GetUserId()!.Value;
            return Ok(await _service.GetHistoryAsync(userId));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        [UserMod]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
        {
            var userId = GetUserId()!.Value;
            var created = await _service.CreateAsync(userId, dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        private int? GetUserId()
        {
            var raw = HttpContext.Session.GetString("userId");
            return int.TryParse(raw, out var id) ? id : null;
        }
    }
}
