using HeavenlyKingdom.Api.Filters;
using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HeavenlyKingdom.Api.Controllers
{
    [Route("api/admin/holidays")]
    [ApiController]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayService _holidayService;
        public HolidayController(IHolidayService holidayService) =>
            _holidayService = holidayService;

        // GET /api/admin/holidays
        [HttpGet]
        [AdminMod]
        public async Task<IActionResult> GetAll()
        {
            var result = await _holidayService.GetAllAsync();
            return Ok(result);
        }

        // POST /api/admin/holidays
        [HttpPost]
        [AdminMod]
        public async Task<IActionResult> Create([FromBody] CreateHolidayDto dto)
        {
            var result = await _holidayService.CreateAsync(dto);
            return Created($"/api/admin/holidays/{result.Id}", result);
        }

        // DELETE /api/admin/holidays/{id}
        [HttpDelete("{id}")]
        [AdminMod]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _holidayService.DeleteAsync(id);
            if (!success) return NotFound(new { Message = "Holiday not found" });
            return NoContent();
        }
    }
}
