using HeavenlyKingdom.DataAccess.Context;
using HeavenlyKingdom.DataAccess.Interfaces;
using HeavenlyKingdom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeavenlyKingdom.DataAccess.Repositories
{
    public class HolidayRepository : IHolidayRepository
    {
        private readonly AppDbContext _context;
        public HolidayRepository(AppDbContext context) => _context = context;

        public async Task<List<Holiday>> GetAllAsync() =>
            await _context.Holidays
                .OrderBy(h => h.Date)
                .ToListAsync();

        public async Task<Holiday> AddAsync(Holiday holiday)
        {
            _context.Holidays.Add(holiday);
            await _context.SaveChangesAsync();
            return holiday;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var holiday = await _context.Holidays.FindAsync(id);
            if (holiday == null) return false;
            _context.Holidays.Remove(holiday);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
