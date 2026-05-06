using HeavenlyKingdom.Domain.Entities;

namespace HeavenlyKingdom.DataAccess.Interfaces
{
    public interface IHolidayRepository
    {
        Task<List<Holiday>> GetAllAsync();
        Task<Holiday> AddAsync(Holiday holiday);
        Task<bool> DeleteAsync(int id);
    }
}
