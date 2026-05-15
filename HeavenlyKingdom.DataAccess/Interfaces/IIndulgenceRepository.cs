using HeavenlyKingdom.Domain.Entities;

namespace HeavenlyKingdom.DataAccess.Interfaces
{
    public interface IIndulgenceRepository
    {
        Task<List<Indulgence>> GetByUserIdAsync(int userId);
        Task<Indulgence> AddAsync(Indulgence indulgence);
    }
}
