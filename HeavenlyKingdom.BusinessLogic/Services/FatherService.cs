using AutoMapper;
using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.DataAccess.Interfaces;
using HeavenlyKingdom.Domain.DTOs;
using HeavenlyKingdom.Domain.Entities;

namespace HeavenlyKingdom.BusinessLogic.Services
{
    public class FatherService : IFatherService
    {
        private readonly IFatherRepository _repo;
        private readonly IOrderRepository _orderRepo;
        private readonly IMapper _mapper;

        public FatherService(IFatherRepository repo, IOrderRepository orderRepo, IMapper mapper)
        {
            _repo = repo;
            _orderRepo = orderRepo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FatherDto>> GetAllAsync()
        {
            var fathers = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<FatherDto>>(fathers);
        }

        public async Task<FatherDto?> GetByIdAsync(int id)
        {
            var father = await _repo.GetByIdAsync(id);
            return father == null ? null : _mapper.Map<FatherDto>(father);
        }

        public async Task<FatherDto?> GetByUserIdAsync(int userId)
        {
            var father = await _repo.GetByUserIdAsync(userId);
            return father == null ? null : _mapper.Map<FatherDto>(father);
        }

        public async Task<FatherDto> CreateAsync(CreateFatherDto dto)
        {
            var father = _mapper.Map<Father>(dto);
            var created = await _repo.AddAsync(father);
            return _mapper.Map<FatherDto>(created);
        }

        public async Task<FatherDto?> UpdateAsync(int id, UpdateFatherDto dto)
        {
            var father = _mapper.Map<Father>(dto);
            father.Id = id;
            var updated = await _repo.UpdateAsync(father);
            return updated == null ? null : _mapper.Map<FatherDto>(updated);
        }

        public async Task<FatherDto?> UpdateProfileAsync(int userId, UpdateFatherProfileDto dto)
        {
            var father = await _repo.GetByUserIdAsync(userId);
            if (father == null) return null;
            _mapper.Map(dto, father);
            var updated = await _repo.UpdateAsync(father);
            return updated == null ? null : _mapper.Map<FatherDto>(updated);
        }

        public async Task<bool> DeleteAsync(int id) =>
            await _repo.DeleteAsync(id);

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task EnsureProfileAsync(int userId, string name, string lastName)
        {
            var existing = await _repo.GetByUserIdAsync(userId);
            if (existing != null) return;

            await _repo.AddAsync(new Domain.Entities.Father
            {
                Name     = name,
                LastName = lastName,
                UserId   = userId,
            });
        }
    }
}
