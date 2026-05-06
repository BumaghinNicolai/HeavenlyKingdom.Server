using HeavenlyKingdom.Domain.DTOs;

namespace HeavenlyKingdom.BusinessLogic.Interfaces
{
    public interface IHolidayService
    {
        Task<List<HolidayDto>> GetAllAsync();
        Task<HolidayDto> CreateAsync(CreateHolidayDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
