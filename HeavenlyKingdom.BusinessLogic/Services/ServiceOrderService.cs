using AutoMapper;
using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.DataAccess.Interfaces;
using HeavenlyKingdom.Domain.DTOs;
using HeavenlyKingdom.Domain.Entities;

namespace HeavenlyKingdom.BusinessLogic.Services
{
    public class ServiceOrderService : IServiceOrderService
    {
        private readonly IServiceOrderRepository _repo;
        private readonly IFatherRepository _fatherRepo;
        private readonly IMapper _mapper;

        public ServiceOrderService(
            IServiceOrderRepository repo,
            IFatherRepository fatherRepo,
            IMapper mapper)
        {
            _repo = repo;
            _fatherRepo = fatherRepo;
            _mapper = mapper;
        }

        public async Task<List<ServiceOrderDto>> GetByUserIdAsync(int userId)
        {
            var orders = await _repo.GetByUserIdAsync(userId);
            return _mapper.Map<List<ServiceOrderDto>>(orders);
        }

        public async Task<List<ServiceOrderDto>> GetByFatherAsync(int userId)
        {
            var father = await _fatherRepo.GetByUserIdAsync(userId);
            if (father == null) return new List<ServiceOrderDto>();
            var orders = await _repo.GetByFatherIdAsync(father.Id);
            return _mapper.Map<List<ServiceOrderDto>>(orders);
        }

        public async Task<ServiceOrderDto> CreateAsync(int userId, CreateServiceOrderDto dto)
        {
            var order = new ServiceOrder
            {
                UserId = userId,
                FatherId = dto.FatherId,
                ServiceId = dto.ServiceId,
                ServiceName = dto.ServiceName,
                ClientName = dto.ClientName,
                ClientEmail = dto.ClientEmail,
                Date = dto.Date,
                Time = dto.Time,
                Notes = dto.Notes,
                Status = "pending",
                CreatedAt = DateTime.UtcNow
            };
            var created = await _repo.AddAsync(order);
            return _mapper.Map<ServiceOrderDto>(created);
        }

        public async Task<ServiceOrderDto?> CompleteAsync(int id, int fatherUserId)
        {
            var father = await _fatherRepo.GetByUserIdAsync(fatherUserId);
            if (father == null) return null;

            var order = await _repo.GetByIdAsync(id);
            if (order == null || order.FatherId != father.Id) return null;

            var updated = await _repo.UpdateStatusAsync(id, "completed");
            return updated == null ? null : _mapper.Map<ServiceOrderDto>(updated);
        }
    }
}
