using HeavenlyKingdom.Api.Filters;
using HeavenlyKingdom.Api.Services;
using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HeavenlyKingdom.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtService _jwtService;

        public UserController(IUserService userService, JwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpGet("all")]
        [AdminMod]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [AdminMod]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound(new { Message = $"User {id} not found" });
            return Ok(user);
        }

        [HttpGet("session/refresh")]
        [UserMod]
        public IActionResult RefreshSession()
        {
            return Ok(new { Message = "Token valid" });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest(new { Message = "Email and password are required" });

            var result = await _userService.RegisterAsync(dto);
            if (result == null) return Conflict(new { Message = "Email already taken" });

            var token = _jwtService.GenerateToken(result);
            return Created($"/api/user/{result.Id}", new AuthResponseDto { Token = token, User = result });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _userService.LoginAsync(dto);
            if (result == null) return Unauthorized(new { Message = "Invalid email or password" });

            var token = _jwtService.GenerateToken(result);
            return Ok(new AuthResponseDto { Token = token, User = result });
        }

        [HttpPut("me")]
        [UserMod]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto dto)
        {
            var raw = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(raw, out var userId)) return Unauthorized();
            var result = await _userService.UpdateProfileAsync(userId, dto);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost("logout")]
        [UserMod]
        public IActionResult Logout()
        {
            return Ok(new { Message = "Logged out" });
        }

        [HttpDelete("{id}")]
        [AdminMod]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _userService.DeleteAsync(id);
            if (!success) return NotFound(new { Message = $"User {id} not found" });
            return NoContent();
        }
    }
}
