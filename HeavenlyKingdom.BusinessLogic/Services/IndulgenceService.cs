using AutoMapper;
using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.DataAccess.Interfaces;
using HeavenlyKingdom.Domain.DTOs;
using HeavenlyKingdom.Domain.Entities;

namespace HeavenlyKingdom.BusinessLogic.Services
{
    public class IndulgenceService : IIndulgenceService
    {
        private readonly IIndulgenceRepository _repo;
        private readonly IMapper _mapper;

        public IndulgenceService(IIndulgenceRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<IndulgenceDto>> GetHistoryAsync(int userId)
        {
            var indulgences = await _repo.GetByUserIdAsync(userId);
            return _mapper.Map<List<IndulgenceDto>>(indulgences);
        }

        public async Task<IndulgenceDto> PurchaseAsync(int? userId, PurchaseIndulgenceDto dto)
        {
            var indulgence = new Indulgence
            {
                UserId = userId,
                Sin = dto.Sin,
                Gravity = dto.Gravity,
                Price = dto.Price,
                PurchasedAt = DateTime.UtcNow
            };

            var created = await _repo.AddAsync(indulgence);
            return _mapper.Map<IndulgenceDto>(created);
        }
    }
}
