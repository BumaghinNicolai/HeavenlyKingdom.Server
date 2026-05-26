using HeavenlyKingdom.Domain.DTOs;

namespace HeavenlyKingdom.BusinessLogic.Interfaces
{
    public interface IServiceOrderService
    {
        Task<List<ServiceOrderDto>> GetByUserIdAsync(int userId);
        Task<List<ServiceOrderDto>> GetByFatherAsync(int userId);
        Task<ServiceOrderDto> CreateAsync(int userId, CreateServiceOrderDto dto);
        Task<ServiceOrderDto?> CompleteAsync(int id, int fatherUserId);
    }
}
