using HeavenlyKingdom.Domain.DTOs;

namespace HeavenlyKingdom.BusinessLogic.Interfaces
{
    public interface IDonationService
    {
        Task<DonationGoalDto?> GetGoalAsync();
        Task<DonationGoalDto> UpdateGoalAsync(UpdateDonationGoalDto dto);
        Task<DonationGoalDto> AddProgressAsync(AddProgressDto dto);
        Task<DonationGoalDto> ResetAsync();
    }
}
