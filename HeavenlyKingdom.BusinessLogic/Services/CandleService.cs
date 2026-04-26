using AutoMapper;
using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.DataAccess.Interfaces;
using HeavenlyKingdom.Domain.DTOs;
using HeavenlyKingdom.Domain.Entities;

namespace HeavenlyKingdom.BusinessLogic.Services
{
    public class CandleService : ICandleService
    {
        private readonly ICandleRepository _repo;
        private readonly IMapper _mapper;

        public CandleService(ICandleRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CandleDto>> GetAllAsync()
        {
            var candles = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<CandleDto>>(candles);
        }

        public async Task<CandleDto?> GetByIdAsync(int id)
        {
            var candle = await _repo.GetByIdAsync(id);
            return candle == null ? null : _mapper.Map<CandleDto>(candle);
        }

        public async Task<CandleDto> CreateAsync(CreateCandleDto dto)
        {
            var candle = _mapper.Map<Candle>(dto);
            var created = await _repo.AddAsync(candle);
            return _mapper.Map<CandleDto>(created);
        }

        public async Task<CandleDto?> UpdateAsync(int id, UpdateCandleDto dto)
        {
            var candle = _mapper.Map<Candle>(dto);
            candle.Id = id;
            var updated = await _repo.UpdateAsync(candle);
            return updated == null ? null : _mapper.Map<CandleDto>(updated);
        }

        public async Task<bool> DeleteAsync(int id) =>
            await _repo.DeleteAsync(id);
    }
}