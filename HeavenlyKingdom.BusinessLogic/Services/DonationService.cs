using AutoMapper;
using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.DataAccess.Interfaces;
using HeavenlyKingdom.Domain.DTOs;

namespace HeavenlyKingdom.BusinessLogic.Services
{
    public class DonationService : IDonationService
    {
        private readonly IDonationRepository _repo;
        private readonly IMapper _mapper;

        public DonationService(IDonationRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<DonationGoalDto?> GetGoalAsync()
        {
            var goal = await _repo.GetGoalAsync();
            return goal == null ? null : _mapper.Map<DonationGoalDto>(goal);
        }

        public async Task<DonationGoalDto> UpdateGoalAsync(UpdateDonationGoalDto dto)
        {
            var goal = await _repo.GetGoalAsync()
                ?? throw new InvalidOperationException("Donation goal not found");

            goal.Title = dto.Title;
            goal.Target = dto.Target;

            var updated = await _repo.UpdateGoalAsync(goal);
            return _mapper.Map<DonationGoalDto>(updated);
        }

        public async Task<DonationGoalDto> AddProgressAsync(AddProgressDto dto)
        {
            var updated = await _repo.AddProgressAsync(dto.Amount);
            return _mapper.Map<DonationGoalDto>(updated);
        }

        public async Task<DonationGoalDto> ResetAsync()
        {
            var updated = await _repo.ResetAsync();
            return _mapper.Map<DonationGoalDto>(updated);
        }
    }
}
