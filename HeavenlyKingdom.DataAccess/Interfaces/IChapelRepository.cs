using HeavenlyKingdom.Domain.Entities;

namespace HeavenlyKingdom.DataAccess.Interfaces
{
    public interface IChapelRepository
    {
        Task<List<ChapelCandle>> GetActiveAsync();
        Task<ChapelCandle> AddAsync(ChapelCandle candle);
        Task DeleteAsync(int id);
        Task DeleteExpiredAsync();
        Task<List<ChapelCandle>> GetByUserIdAsync(int userId);
    }
}
