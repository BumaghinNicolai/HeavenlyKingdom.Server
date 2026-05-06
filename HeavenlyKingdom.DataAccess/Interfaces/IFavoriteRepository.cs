using HeavenlyKingdom.Domain.Entities;

namespace HeavenlyKingdom.DataAccess.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<List<Favorite>> GetByUserIdAsync(int userId);
        Task<Favorite> AddAsync(int userId, int productId);
        Task DeleteAsync(int userId, int productId);
        Task<bool> ExistsAsync(int userId, int productId);
    }
}
