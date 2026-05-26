using HeavenlyKingdom.Domain.DTOs;

namespace HeavenlyKingdom.BusinessLogic.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetActiveAsync(int userId);
        Task<List<OrderDto>> GetHistoryAsync(int userId);
        Task<OrderDto?> GetByIdAsync(int id, int userId, bool isAdmin);
        Task<OrderDto> CreateAsync(int userId, CreateOrderDto dto);
        Task<List<ProductSalesDto>> GetProductSalesAsync();
    }
}
