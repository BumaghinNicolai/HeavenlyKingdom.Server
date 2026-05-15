using HeavenlyKingdom.Domain.DTOs;

namespace HeavenlyKingdom.BusinessLogic.Interfaces
{
    public interface IChapelService
    {
        Task<List<ChapelCandleDto>> GetActiveAsync();
        Task<ChapelCandleDto?> PlaceCandleAsync(int? userId, PlaceCandleDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
