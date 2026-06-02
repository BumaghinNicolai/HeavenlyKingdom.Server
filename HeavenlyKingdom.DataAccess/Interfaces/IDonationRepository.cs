using HeavenlyKingdom.Domain.Entities;

namespace HeavenlyKingdom.DataAccess.Interfaces
{
    public interface IDonationRepository
    {
        Task<DonationGoal?> GetGoalAsync();
        Task<DonationGoal> UpdateGoalAsync(DonationGoal goal);
        Task<DonationGoal> AddProgressAsync(decimal amount);
        Task<DonationGoal> ResetAsync();
        Task<List<Donation>> GetHistoryAsync();
    }
}
