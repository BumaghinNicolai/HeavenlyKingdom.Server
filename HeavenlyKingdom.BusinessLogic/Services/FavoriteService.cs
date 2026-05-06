using AutoMapper;
using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.DataAccess.Interfaces;
using HeavenlyKingdom.Domain.DTOs;

namespace HeavenlyKingdom.BusinessLogic.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _repo;
        private readonly IMapper _mapper;

        public FavoriteService(IFavoriteRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<FavoriteDto>> GetByUserIdAsync(int userId)
        {
            var favorites = await _repo.GetByUserIdAsync(userId);
            return _mapper.Map<List<FavoriteDto>>(favorites);
        }

        public async Task<FavoriteDto?> AddAsync(int userId, int productId)
        {
            if (await _repo.ExistsAsync(userId, productId)) return null;
            var favorite = await _repo.AddAsync(userId, productId);
            return _mapper.Map<FavoriteDto>(favorite);
        }

        public async Task<bool> DeleteAsync(int userId, int productId)
        {
            if (!await _repo.ExistsAsync(userId, productId)) return false;
            await _repo.DeleteAsync(userId, productId);
            return true;
        }
    }
}
