using AutoMapper;
using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.DataAccess.Interfaces;
using HeavenlyKingdom.Domain.DTOs;
using HeavenlyKingdom.Domain.Entities;

namespace HeavenlyKingdom.BusinessLogic.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly IHolidayRepository _repo;
        private readonly IMapper _mapper;

        public HolidayService(IHolidayRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<HolidayDto>> GetAllAsync()
        {
            var holidays = await _repo.GetAllAsync();
            return _mapper.Map<List<HolidayDto>>(holidays);
        }

        public async Task<HolidayDto> CreateAsync(CreateHolidayDto dto)
        {
            var holiday = new Holiday
            {
                Name = dto.Name,
                Date = dto.Date
            };
            var created = await _repo.AddAsync(holiday);
            return _mapper.Map<HolidayDto>(created);
        }

        public async Task<bool> DeleteAsync(int id) =>
            await _repo.DeleteAsync(id);
    }
}
