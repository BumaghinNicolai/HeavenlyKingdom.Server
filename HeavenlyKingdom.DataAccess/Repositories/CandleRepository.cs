using HeavenlyKingdom.DataAccess.Context;
using HeavenlyKingdom.DataAccess.Interfaces;
using HeavenlyKingdom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeavenlyKingdom.DataAccess.Repositories
{
    public class CandleRepository : ICandleRepository
    {
        private readonly AppDbContext _db;
        public CandleRepository(AppDbContext db) => _db = db;

        public async Task<IEnumerable<Candle>> GetAllAsync() =>
            await _db.Candles.ToListAsync();

        public async Task<Candle?> GetByIdAsync(int id) =>
            await _db.Candles.FindAsync(id);

        public async Task<Candle> AddAsync(Candle candle)
        {
            await _db.Candles.AddAsync(candle);
            await _db.SaveChangesAsync();
            return candle;
        }

        public async Task<Candle?> UpdateAsync(Candle candle)
        {
            var existing = await _db.Candles.FindAsync(candle.Id);
            if (existing == null) return null;
            _db.Entry(existing).CurrentValues.SetValues(candle);
            await _db.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var candle = await _db.Candles.FindAsync(id);
            if (candle == null) return false;
            _db.Candles.Remove(candle);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}