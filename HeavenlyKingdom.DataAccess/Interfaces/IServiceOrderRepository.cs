using HeavenlyKingdom.Domain.Entities;

namespace HeavenlyKingdom.DataAccess.Interfaces
{
    public interface IServiceOrderRepository
    {
        Task<List<ServiceOrder>> GetByUserIdAsync(int userId);
        Task<List<ServiceOrder>> GetByFatherIdAsync(int fatherId);
        Task<ServiceOrder?> GetByIdAsync(int id);
        Task<ServiceOrder> AddAsync(ServiceOrder order);
        Task<ServiceOrder?> UpdateStatusAsync(int id, string status);
    }
}
