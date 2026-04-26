using HeavenlyKingdom.Domain.DTOs;

namespace HeavenlyKingdom.BusinessLogic.Interfaces
{
    public interface ICandleService
    {
        Task<IEnumerable<CandleDto>> GetAllAsync();
        Task<CandleDto?> GetByIdAsync(int id);
        Task<CandleDto> CreateAsync(CreateCandleDto dto);
        Task<CandleDto?> UpdateAsync(int id, UpdateCandleDto dto);
        Task<bool> DeleteAsync(int id);
    }
}