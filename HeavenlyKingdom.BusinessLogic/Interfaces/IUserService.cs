using HeavenlyKingdom.Domain.DTOs;

namespace HeavenlyKingdom.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto?> RegisterAsync(RegisterDto dto);
        Task<UserResponseDto?> LoginAsync(LoginDto dto);
        Task<UserResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<UserResponseDto>> GetAllAsync();
        Task<bool> DeleteAsync(int id);
    }
}