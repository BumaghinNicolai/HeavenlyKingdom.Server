using AutoMapper;
using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.DataAccess.Interfaces;
using HeavenlyKingdom.Domain.DTOs;
using HeavenlyKingdom.Domain.Entities;

namespace HeavenlyKingdom.BusinessLogic.Services
{
    public class ChapelService : IChapelService
    {
        private readonly IChapelRepository _repo;
        private readonly IMapper _mapper;

        public ChapelService(IChapelRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<ChapelCandleDto>> GetActiveAsync()
        {
            var candles = await _repo.GetActiveAsync();
            return _mapper.Map<List<ChapelCandleDto>>(candles);
        }

        public async Task<ChapelCandleDto?> PlaceCandleAsync(int? userId, PlaceCandleDto dto)
        {
            var active = await _repo.GetActiveAsync();

            // Проверяем что слот свободен
            if (active.Any(c => c.SlotIndex == dto.SlotIndex))
                return null;

            var duration = dto.Type switch
            {
                "large" => TimeSpan.FromHours(4),
                "festive" => TimeSpan.FromHours(12),
                _ => TimeSpan.FromHours(1)  // simple
            };

            var candle = new ChapelCandle
            {
                UserId = userId,
                Type = dto.Type,
                SlotIndex = dto.SlotIndex,
                Note = dto.Note,
                Intention = dto.Intention,
                PlacedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow + duration
            };

            var created = await _repo.AddAsync(candle);
            return _mapper.Map<ChapelCandleDto>(created);
        }

        public async Task<List<ChapelCandleDto>> GetMyAsync(int userId)
        {
            var candles = await _repo.GetByUserIdAsync(userId);
            return _mapper.Map<List<ChapelCandleDto>>(candles);
        }

        public async Task<bool?> DeleteAsync(int id, int? userId, bool isAdmin)
        {
            var candles = await _repo.GetActiveAsync();
            var candle = candles.FirstOrDefault(c => c.Id == id);
            if (candle == null) return false;
            if (!isAdmin && candle.UserId != userId) return null;
            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
