using HeavenlyKingdom.Domain.DTOs;

namespace HeavenlyKingdom.BusinessLogic.Interfaces
{
    public interface IIndulgenceService
    {
        Task<List<IndulgenceDto>> GetHistoryAsync(int userId);
        Task<IndulgenceDto> PurchaseAsync(int? userId, PurchaseIndulgenceDto dto);
    }
}
