using HeavenlyKingdom.Domain.DTOs;
using HeavenlyKingdom.Domain.Enums;

namespace HeavenlyKingdom.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto?> RegisterAsync(RegisterDto dto);
        Task<UserResponseDto?> LoginAsync(LoginDto dto);
        Task<UserResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<UserResponseDto>> GetAllAsync();
        Task<bool> DeleteAsync(int id);
        Task<UserResponseDto?> UpdateAsync(int id, UpdateUserDto dto);
        Task<UserResponseDto?> SetRoleAsync(int id, UserRole role);
        Task<UserResponseDto?> AdminUpdateAsync(int id, AdminUpdateUserDto dto);
    }
}