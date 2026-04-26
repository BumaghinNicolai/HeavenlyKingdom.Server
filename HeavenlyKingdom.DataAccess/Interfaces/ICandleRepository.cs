using HeavenlyKingdom.Domain.Entities;

namespace HeavenlyKingdom.DataAccess.Interfaces
{
    public interface ICandleRepository
    {
        Task<IEnumerable<Candle>> GetAllAsync();
        Task<Candle?> GetByIdAsync(int id);
        Task<Candle> AddAsync(Candle candle);
        Task<Candle?> UpdateAsync(Candle candle);
        Task<bool> DeleteAsync(int id);
    }
}