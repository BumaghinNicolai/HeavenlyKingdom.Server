using HeavenlyKingdom.DataAccess.Context;
using HeavenlyKingdom.DataAccess.Interfaces;
using HeavenlyKingdom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeavenlyKingdom.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync(string? search, string? category, decimal? minPrice, decimal? maxPrice)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(p => p.Name.Contains(search));

            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(p => p.Category.Name == category);

            if (minPrice != null)
                query = query.Where(p => p.Price >= minPrice);

            if (maxPrice != null)
                query = query.Where(p => p.Price <= maxPrice);

            return await query.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id) =>
            await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

        public async Task<Product> CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> UpdateAsync(int id, Product updated)
        {
            var existing = await _context.Products.FindAsync(id);
            if (existing == null) return null;

            existing.Name = updated.Name;
            existing.Price = updated.Price;
            existing.Img = updated.Img;
            existing.IsNew = updated.IsNew;
            existing.OnSale = updated.OnSale;
            existing.SalePrice = updated.SalePrice;
            existing.CategoryId = updated.CategoryId;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}