using AutoMapper;
using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.DataAccess.Interfaces;
using HeavenlyKingdom.Domain.DTOs;
using HeavenlyKingdom.Domain.Entities;

namespace HeavenlyKingdom.BusinessLogic.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _repo;
        private readonly IMapper _mapper;

        public AddressService(IAddressRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<AddressDto>> GetByUserIdAsync(int userId)
        {
            var addresses = await _repo.GetByUserIdAsync(userId);
            return _mapper.Map<List<AddressDto>>(addresses);
        }

        public async Task<AddressDto> CreateAsync(int userId, CreateAddressDto dto)
        {
            if (dto.IsDefault)
                await _repo.SetDefaultAsync(0, userId); // сбрасываем IsDefault у всех

            var address = new Address
            {
                UserId = userId,
                City = dto.City,
                Street = dto.Street,
                House = dto.House,
                Apartment = dto.Apartment,
                IsDefault = dto.IsDefault
            };

            var created = await _repo.AddAsync(address);
            return _mapper.Map<AddressDto>(created);
        }

        public async Task<AddressDto?> UpdateAsync(int id, int userId, UpdateAddressDto dto)
        {
            var address = await _repo.GetByIdAsync(id);
            if (address == null || address.UserId != userId) return null;

            address.City = dto.City;
            address.Street = dto.Street;
            address.House = dto.House;
            address.Apartment = dto.Apartment;
            address.IsDefault = dto.IsDefault;

            await _repo.UpdateAsync(address);

            if (dto.IsDefault)
                await _repo.SetDefaultAsync(id, userId);

            return _mapper.Map<AddressDto>(address);
        }

        public async Task<bool> DeleteAsync(int id, int userId)
        {
            var address = await _repo.GetByIdAsync(id);
            if (address == null || address.UserId != userId) return false;
            await _repo.DeleteAsync(id);
            return true;
        }

        public async Task<bool> SetDefaultAsync(int addressId, int userId)
        {
            var address = await _repo.GetByIdAsync(addressId);
            if (address == null || address.UserId != userId) return false;
            await _repo.SetDefaultAsync(addressId, userId);
            return true;
        }
    }
}
