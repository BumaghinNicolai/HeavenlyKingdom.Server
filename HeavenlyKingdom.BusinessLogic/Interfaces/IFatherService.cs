using HeavenlyKingdom.Domain.DTOs;

namespace HeavenlyKingdom.BusinessLogic.Interfaces
{
    public interface IFatherService
    {
        Task<IEnumerable<FatherDto>> GetAllAsync();
        Task<FatherDto?> GetByIdAsync(int id);
        Task<FatherDto?> GetByUserIdAsync(int userId);
        Task<FatherDto> CreateAsync(CreateFatherDto dto);
        Task<FatherDto?> UpdateAsync(int id, UpdateFatherDto dto);
        Task<FatherDto?> UpdateProfileAsync(int userId, UpdateFatherProfileDto dto);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
    }
}