using HeavenlyKingdom.DataAccess.Interfaces;
using HeavenlyKingdom.DataAccess.Context;
using HeavenlyKingdom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeavenlyKingdom.DataAccess.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly AppDbContext _context;
        public CartItemRepository(AppDbContext context) => _context = context;

        public async Task<List<CartItem>> GetBySessionAsync(string sessionId) =>
            await _context.CartItems
                .Include(ci => ci.Product).ThenInclude(p => p.Category)
                .Where(ci => ci.SessionId == sessionId)
                .ToListAsync();

        public async Task<CartItem?> GetByIdAsync(int id) =>
            await _context.CartItems.Include(ci => ci.Product)
                .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(ci => ci.Id == id);

        public async Task<CartItem> AddAsync(CartItem item)
        {
            // Если такой товар уже в корзине — увеличиваем количество
            var existing = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.SessionId == item.SessionId && ci.ProductId == item.ProductId);

            if (existing != null)
            {
                existing.Quantity += item.Quantity;
                await _context.SaveChangesAsync();
                return existing;
            }

            _context.CartItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<CartItem?> UpdateQuantityAsync(int id, int quantity)
        {
            var item = await _context.CartItems.FindAsync(id);
            if (item == null) return null;
            item.Quantity = quantity;
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.CartItems.FindAsync(id);
            if (item == null) return false;
            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClearSessionAsync(string sessionId)
        {
            var items = await _context.CartItems
                .Where(ci => ci.SessionId == sessionId).ToListAsync();
            _context.CartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}