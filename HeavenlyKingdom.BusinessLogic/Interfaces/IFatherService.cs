using HeavenlyKingdom.Domain.DTOs;

namespace HeavenlyKingdom.BusinessLogic.Interfaces
{
    public interface IFatherService
    {
        Task<IEnumerable<FatherDto>> GetAllAsync();
        Task<FatherDto?> GetByIdAsync(int id);
        Task<FatherDto> CreateAsync(CreateFatherDto dto);
        Task<FatherDto?> UpdateAsync(int id, UpdateFatherDto dto);
        Task<bool> DeleteAsync(int id);
    }
}