using HeavenlyKingdom.DataAccess.Context;
using HeavenlyKingdom.DataAccess.Interfaces;
using HeavenlyKingdom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeavenlyKingdom.DataAccess.Repositories
{
    public class ChapelRepository : IChapelRepository
    {
        private readonly AppDbContext _context;
        public ChapelRepository(AppDbContext context) => _context = context;

        public async Task<List<ChapelCandle>> GetActiveAsync() =>
            await _context.ChapelCandles
                .Where(c => c.ExpiresAt > DateTime.UtcNow)
                .OrderBy(c => c.SlotIndex)
                .ToListAsync();

        public async Task<ChapelCandle> AddAsync(ChapelCandle candle)
        {
            _context.ChapelCandles.Add(candle);
            await _context.SaveChangesAsync();
            return candle;
        }

        public async Task DeleteAsync(int id)
        {
            var candle = await _context.ChapelCandles.FindAsync(id);
            if (candle != null)
            {
                _context.ChapelCandles.Remove(candle);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ChapelCandle>> GetByUserIdAsync(int userId) =>
            await _context.ChapelCandles
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.PlacedAt)
                .ToListAsync();

        public async Task DeleteExpiredAsync()
        {
            var expired = await _context.ChapelCandles
                .Where(c => c.ExpiresAt <= DateTime.UtcNow)
                .ToListAsync();

            _context.ChapelCandles.RemoveRange(expired);
            await _context.SaveChangesAsync();
        }
    }
}
