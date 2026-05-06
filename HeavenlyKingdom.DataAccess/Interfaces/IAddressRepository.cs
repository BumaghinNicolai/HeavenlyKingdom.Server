using HeavenlyKingdom.Domain.Entities;

namespace HeavenlyKingdom.DataAccess.Interfaces
{
    public interface IAddressRepository
    {
        Task<List<Address>> GetByUserIdAsync(int userId);
        Task<Address?> GetByIdAsync(int id);
        Task<Address> AddAsync(Address address);
        Task<Address> UpdateAsync(Address address);
        Task DeleteAsync(int id);
        Task SetDefaultAsync(int addressId, int userId);
    }
}
