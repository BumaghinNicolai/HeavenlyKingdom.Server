using HeavenlyKingdom.DataAccess.Context;
using HeavenlyKingdom.DataAccess.Interfaces;
using HeavenlyKingdom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeavenlyKingdom.DataAccess.Repositories
{
    public class FatherRepository : IFatherRepository
    {
        private readonly AppDbContext _db;
        public FatherRepository(AppDbContext db) => _db = db;

        public async Task<IEnumerable<Father>> GetAllAsync() =>
            await _db.Fathers.ToListAsync();

        public async Task<Father?> GetByIdAsync(int id) =>
            await _db.Fathers.FindAsync(id);

        public async Task<Father> AddAsync(Father father)
        {
            await _db.Fathers.AddAsync(father);
            await _db.SaveChangesAsync();
            return father;
        }

        public async Task<Father?> UpdateAsync(Father father)
        {
            var existing = await _db.Fathers.FindAsync(father.Id);
            if (existing == null) return null;
            _db.Entry(existing).CurrentValues.SetValues(father);
            await _db.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var father = await _db.Fathers.FindAsync(id);
            if (father == null) return false;
            _db.Fathers.Remove(father);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}