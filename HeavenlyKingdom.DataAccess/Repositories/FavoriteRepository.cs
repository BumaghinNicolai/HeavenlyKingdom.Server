using HeavenlyKingdom.DataAccess.Context;
using HeavenlyKingdom.DataAccess.Interfaces;
using HeavenlyKingdom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeavenlyKingdom.DataAccess.Repositories
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly AppDbContext _context;
        public FavoriteRepository(AppDbContext context) => _context = context;

        public async Task<List<Favorite>> GetByUserIdAsync(int userId) =>
            await _context.Favorites
                .Include(f => f.Product).ThenInclude(p => p.Category)
                .Where(f => f.UserId == userId)
                .ToListAsync();

        public async Task<Favorite> AddAsync(int userId, int productId)
        {
            var favorite = new Favorite { UserId = userId, ProductId = productId };
            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();
            return await _context.Favorites
                .Include(f => f.Product).ThenInclude(p => p.Category)
                .FirstAsync(f => f.Id == favorite.Id);
        }

        public async Task DeleteAsync(int userId, int productId)
        {
            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.ProductId == productId);
            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int userId, int productId) =>
            await _context.Favorites
                .AnyAsync(f => f.UserId == userId && f.ProductId == productId);
    }
}
