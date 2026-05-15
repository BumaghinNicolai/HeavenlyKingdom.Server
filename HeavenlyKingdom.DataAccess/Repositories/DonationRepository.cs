using HeavenlyKingdom.DataAccess.Context;
using HeavenlyKingdom.DataAccess.Interfaces;
using HeavenlyKingdom.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HeavenlyKingdom.DataAccess.Repositories
{
    public class DonationRepository : IDonationRepository
    {
        private readonly AppDbContext _context;
        public DonationRepository(AppDbContext context) => _context = context;

        public async Task<DonationGoal?> GetGoalAsync() =>
            await _context.DonationGoals.FirstOrDefaultAsync();

        public async Task<DonationGoal> UpdateGoalAsync(DonationGoal goal)
        {
            _context.DonationGoals.Update(goal);
            await _context.SaveChangesAsync();
            return goal;
        }

        public async Task<DonationGoal> AddProgressAsync(decimal amount)
        {
            var goal = await _context.DonationGoals.FirstOrDefaultAsync()
                ?? throw new InvalidOperationException("Donation goal not found");

            goal.Current += amount;
            await _context.SaveChangesAsync();
            return goal;
        }

        public async Task<DonationGoal> ResetAsync()
        {
            var goal = await _context.DonationGoals.FirstOrDefaultAsync()
                ?? throw new InvalidOperationException("Donation goal not found");

            goal.Current = 0;
            await _context.SaveChangesAsync();
            return goal;
        }
    }
}
