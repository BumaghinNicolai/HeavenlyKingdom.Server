using HeavenlyKingdom.DataAccess.Context;
using HeavenlyKingdom.DataAccess.Interfaces;
using HeavenlyKingdom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeavenlyKingdom.DataAccess.Repositories
{
    public class IndulgenceRepository : IIndulgenceRepository
    {
        private readonly AppDbContext _context;
        public IndulgenceRepository(AppDbContext context) => _context = context;

        public async Task<List<Indulgence>> GetByUserIdAsync(int userId) =>
            await _context.Indulgences
                .Where(i => i.UserId == userId)
                .OrderByDescending(i => i.PurchasedAt)
                .ToListAsync();

        public async Task<Indulgence> AddAsync(Indulgence indulgence)
        {
            _context.Indulgences.Add(indulgence);
            await _context.SaveChangesAsync();
            return indulgence;
        }
    }
}
