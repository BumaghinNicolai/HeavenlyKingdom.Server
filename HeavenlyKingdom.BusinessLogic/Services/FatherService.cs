using AutoMapper;
using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.DataAccess.Interfaces;
using HeavenlyKingdom.Domain.DTOs;
using HeavenlyKingdom.Domain.Entities;

namespace HeavenlyKingdom.BusinessLogic.Services
{
    public class FatherService : IFatherService
    {
        private readonly IFatherRepository _repo;
        private readonly IMapper _mapper;

        public FatherService(IFatherRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FatherDto>> GetAllAsync()
        {
            var fathers = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<FatherDto>>(fathers);
        }

        public async Task<FatherDto?> GetByIdAsync(int id)
        {
            var father = await _repo.GetByIdAsync(id);
            return father == null ? null : _mapper.Map<FatherDto>(father);
        }

        public async Task<FatherDto> CreateAsync(CreateFatherDto dto)
        {
            var father = _mapper.Map<Father>(dto);
            var created = await _repo.AddAsync(father);
            return _mapper.Map<FatherDto>(created);
        }

        public async Task<FatherDto?> UpdateAsync(int id, UpdateFatherDto dto)
        {
            var father = _mapper.Map<Father>(dto);
            father.Id = id;
            var updated = await _repo.UpdateAsync(father);
            return updated == null ? null : _mapper.Map<FatherDto>(updated);
        }

        public async Task<bool> DeleteAsync(int id) =>
            await _repo.DeleteAsync(id);
    }
}