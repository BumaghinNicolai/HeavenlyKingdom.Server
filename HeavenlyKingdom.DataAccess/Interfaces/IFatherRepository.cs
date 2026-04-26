using HeavenlyKingdom.Domain.Entities;

namespace HeavenlyKingdom.DataAccess.Interfaces
{
    public interface IFatherRepository
    {
        Task<IEnumerable<Father>> GetAllAsync();
        Task<Father?> GetByIdAsync(int id);
        Task<Father> AddAsync(Father father);
        Task<Father?> UpdateAsync(Father father);
        Task<bool> DeleteAsync(int id);
    }
}