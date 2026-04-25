using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HeavenlyKingdom.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;
        public CartController(ICartService service) => _service = service;

        // GET api/cart?sessionId=abc123
        [HttpGet]
        public async Task<IActionResult> GetCart([FromQuery] string sessionId) =>
            Ok(await _service.GetCartAsync(sessionId));

        // POST api/cart
        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCartDto dto) =>
            Ok(await _service.AddToCartAsync(dto));

        // PUT api/cart/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuantity(int id, UpdateCartItemDto dto)
        {
            var result = await _service.UpdateQuantityAsync(id, dto);
            return result == null ? NotFound() : Ok(result);
        }

        // DELETE api/cart/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveItem(int id) =>
            await _service.RemoveItemAsync(id) ? NoContent() : NotFound();

        // DELETE api/cart/clear?sessionId=abc123
        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart([FromQuery] string sessionId)
        {
            await _service.ClearCartAsync(sessionId);
            return NoContent();
        }
    }
}