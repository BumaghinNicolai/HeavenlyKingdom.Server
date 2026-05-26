using HeavenlyKingdom.Api.Filters;
using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HeavenlyKingdom.Api.Controllers
{
    [ApiController]
    [Route("api/service-orders")]
    public class ServiceOrderController : ControllerBase
    {
        private readonly IServiceOrderService _service;
        public ServiceOrderController(IServiceOrderService service) => _service = service;

        private int? GetUserId()
        {
            var raw = HttpContext.Session.GetString("userId");
            return int.TryParse(raw, out var id) ? id : null;
        }

        [HttpPost]
        [UserMod]
        public async Task<IActionResult> Create([FromBody] CreateServiceOrderDto dto)
        {
            var userId = GetUserId()!.Value;
            var result = await _service.CreateAsync(userId, dto);
            return Created($"/api/service-orders/{result.Id}", result);
        }

        [HttpGet("my")]
        [UserMod]
        public async Task<IActionResult> GetMy()
        {
            var userId = GetUserId()!.Value;
            return Ok(await _service.GetByUserIdAsync(userId));
        }

        [HttpGet("father")]
        [FatherMod]
        public async Task<IActionResult> GetFather()
        {
            var userId = GetUserId()!.Value;
            return Ok(await _service.GetByFatherAsync(userId));
        }

        [HttpPut("{id}/complete")]
        [FatherMod]
        public async Task<IActionResult> Complete(int id)
        {
            var userId = GetUserId()!.Value;
            var result = await _service.CompleteAsync(id, userId);
            return result == null ? NotFound() : Ok(result);
        }
    }
}
