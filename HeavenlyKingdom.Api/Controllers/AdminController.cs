using HeavenlyKingdom.Api.Filters;
using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.Domain.DTOs;
using HeavenlyKingdom.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HeavenlyKingdom.Api.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [AdminMod]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IFatherService _fatherService;
        private readonly IOrderService _orderService;

        public AdminController(IUserService userService, IFatherService fatherService, IOrderService orderService)
        {
            _userService   = userService;
            _fatherService = fatherService;
            _orderService  = orderService;
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var sales = await _orderService.GetProductSalesAsync();
            return Ok(sales);
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpPut("users/{id}/role")]
        public async Task<IActionResult> SetRole(int id, [FromBody] SetRoleDto dto)
        {
            var currentUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == id.ToString())
                return BadRequest(new { Message = "Нельзя изменить свою роль" });

            var result = await _userService.SetRoleAsync(id, dto.Role);
            if (result == null) return NotFound(new { Message = $"User {id} not found" });

            if (dto.Role == UserRole.Father)
                await _fatherService.EnsureProfileAsync(id, result.Name, result.LastName);

            return Ok(result);
        }

        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] AdminUpdateUserDto dto)
        {
            var result = await _userService.AdminUpdateAsync(id, dto);
            if (result == null) return NotFound(new { Message = $"User {id} not found" });
            return Ok(result);
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var currentUserId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == id.ToString())
                return BadRequest(new { Message = "Нельзя удалить свой аккаунт" });

            var success = await _userService.DeleteAsync(id);
            if (!success) return NotFound(new { Message = $"User {id} not found" });
            return NoContent();
        }
    }
}
