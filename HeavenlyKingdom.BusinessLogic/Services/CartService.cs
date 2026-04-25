using AutoMapper;
using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.DataAccess.Interfaces;
using HeavenlyKingdom.Domain.DTOs;
using HeavenlyKingdom.Domain.Entities;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace HeavenlyKingdom.BusinessLogic.Services
{
    public class CartService : ICartService
    {
        private readonly ICartItemRepository _repo;
        private readonly IMapper _mapper;

        public CartService(ICartItemRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<CartItemDto>> GetCartAsync(string sessionId)
        {
            var items = await _repo.GetBySessionAsync(sessionId);
            return _mapper.Map<List<CartItemDto>>(items);
        }

        public async Task<CartItemDto> AddToCartAsync(AddToCartDto dto)
        {
            var entity = _mapper.Map<CartItem>(dto);
            var result = await _repo.AddAsync(entity);
            var full = await _repo.GetByIdAsync(result.Id);
            return _mapper.Map<CartItemDto>(full!);
        }

        public async Task<CartItemDto?> UpdateQuantityAsync(int id, UpdateCartItemDto dto)
        {
            var updated = await _repo.UpdateQuantityAsync(id, dto.Qty);
            return updated == null ? null : _mapper.Map<CartItemDto>(updated);
        }

        public async Task<bool> RemoveItemAsync(int id) =>
            await _repo.DeleteAsync(id);

        public async Task<bool> ClearCartAsync(string sessionId) =>
            await _repo.ClearSessionAsync(sessionId);
    }
}