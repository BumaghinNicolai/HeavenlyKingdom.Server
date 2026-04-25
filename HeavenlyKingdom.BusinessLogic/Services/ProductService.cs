using AutoMapper;
using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.DataAccess.Interfaces;
using HeavenlyKingdom.Domain.DTOs;
using HeavenlyKingdom.Domain.Entities;

namespace HeavenlyKingdom.BusinessLogic.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetAllAsync()
        {
            var products = await _repo.GetAllAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            return product == null ? null : _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateAsync(CreateProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            var created = await _repo.CreateAsync(entity);
            // подгружаем Category чтобы маппер мог взять Category.Name → Cat
            var full = await _repo.GetByIdAsync(created.Id);
            return _mapper.Map<ProductDto>(full!);
        }

        public async Task<ProductDto?> UpdateAsync(int id, UpdateProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            var updated = await _repo.UpdateAsync(id, entity);
            if (updated == null) return null;
            var full = await _repo.GetByIdAsync(updated.Id);
            return _mapper.Map<ProductDto>(full!);
        }

        public async Task<bool> DeleteAsync(int id) =>
            await _repo.DeleteAsync(id);
    }
}