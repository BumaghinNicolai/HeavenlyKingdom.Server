using HeavenlyKingdom.Domain.Entities;

namespace HeavenlyKingdom.DataAccess.Interfaces
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetByUserIdAsync(int userId);
        Task MarkAsReadAsync(int id);
        Task MarkAllAsReadAsync(int userId);
    }
}
