using HeavenlyKingdom.Domain.Entities;

namespace HeavenlyKingdom.DataAccess.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync();
        Task<List<Order>> GetByUserIdAsync(int userId);
        Task<List<Order>> GetActiveByUserIdAsync(int userId);
        Task<List<Order>> GetHistoryByUserIdAsync(int userId);
        Task<Order?> GetByIdAsync(int id);
        Task<Order> AddAsync(Order order);
    }
}
