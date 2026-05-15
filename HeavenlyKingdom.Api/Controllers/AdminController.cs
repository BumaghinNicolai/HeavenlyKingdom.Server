using HeavenlyKingdom.Api.Filters;
using Microsoft.AspNetCore.Mvc;

namespace HeavenlyKingdom.Api.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        [HttpGet("stats")]
        [AdminMod]
        public IActionResult GetStats()
        {
            return Ok(new { Message = "Admin stats - access granted" });
        }
    }
}