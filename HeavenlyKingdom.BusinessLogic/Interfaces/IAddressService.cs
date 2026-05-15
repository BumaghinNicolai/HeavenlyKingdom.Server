using HeavenlyKingdom.Domain.DTOs;

namespace HeavenlyKingdom.BusinessLogic.Interfaces
{
    public interface IAddressService
    {
        Task<List<AddressDto>> GetByUserIdAsync(int userId);
        Task<AddressDto> CreateAsync(int userId, CreateAddressDto dto);
        Task<AddressDto?> UpdateAsync(int id, int userId, UpdateAddressDto dto);
        Task<bool> DeleteAsync(int id, int userId);
        Task<bool> SetDefaultAsync(int addressId, int userId);
    }
}
