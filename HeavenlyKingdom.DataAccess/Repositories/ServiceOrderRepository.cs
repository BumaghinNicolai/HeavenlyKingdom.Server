using HeavenlyKingdom.DataAccess.Context;
using HeavenlyKingdom.DataAccess.Interfaces;
using HeavenlyKingdom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeavenlyKingdom.DataAccess.Repositories
{
    public class ServiceOrderRepository : IServiceOrderRepository
    {
        private readonly AppDbContext _context;
        public ServiceOrderRepository(AppDbContext context) => _context = context;

        public async Task<List<ServiceOrder>> GetByUserIdAsync(int userId) =>
            await _context.ServiceOrders
                .Include(o => o.Father)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

        public async Task<List<ServiceOrder>> GetByFatherIdAsync(int fatherId) =>
            await _context.ServiceOrders
                .Include(o => o.Father)
                .Where(o => o.FatherId == fatherId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

        public async Task<ServiceOrder?> GetByIdAsync(int id) =>
            await _context.ServiceOrders
                .Include(o => o.Father)
                .FirstOrDefaultAsync(o => o.Id == id);

        public async Task<ServiceOrder> AddAsync(ServiceOrder order)
        {
            _context.ServiceOrders.Add(order);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(order.Id) ?? order;
        }

        public async Task<ServiceOrder?> UpdateStatusAsync(int id, string status)
        {
            var order = await _context.ServiceOrders.FindAsync(id);
            if (order == null) return null;
            order.Status = status;
            await _context.SaveChangesAsync();
            return await GetByIdAsync(id);
        }
    }
}
