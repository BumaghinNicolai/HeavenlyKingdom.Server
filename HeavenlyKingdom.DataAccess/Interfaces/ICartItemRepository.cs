using HeavenlyKingdom.Domain.Entities;

namespace HeavenlyKingdom.DataAccess.Interfaces
{
    public interface ICartItemRepository
    {
        Task<List<CartItem>> GetBySessionAsync(string sessionId);
        Task<CartItem?> GetByIdAsync(int id);
        Task<CartItem> AddAsync(CartItem item);
        Task<CartItem?> UpdateQuantityAsync(int id, int quantity);
        Task<bool> DeleteAsync(int id);
        Task<bool> ClearSessionAsync(string sessionId);
    }
}