using HeavenlyKingdom.Domain.DTOs;

namespace HeavenlyKingdom.BusinessLogic.Interfaces
{
    public interface ICartService
    {
        Task<List<CartItemDto>> GetCartAsync(string sessionId);
        Task<CartItemDto> AddToCartAsync(AddToCartDto dto);
        Task<CartItemDto?> UpdateQuantityAsync(int id, UpdateCartItemDto dto);
        Task<bool> RemoveItemAsync(int id);
        Task<bool> ClearCartAsync(string sessionId);
    }
}