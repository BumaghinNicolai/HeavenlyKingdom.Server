using HeavenlyKingdom.Domain.DTOs;

namespace HeavenlyKingdom.BusinessLogic.Interfaces
{
    public interface IFavoriteService
    {
        Task<List<FavoriteDto>> GetByUserIdAsync(int userId);
        Task<FavoriteDto?> AddAsync(int userId, int productId);
        Task<bool> DeleteAsync(int userId, int productId);
    }
}
