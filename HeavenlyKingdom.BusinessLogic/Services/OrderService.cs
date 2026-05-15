using AutoMapper;
using HeavenlyKingdom.BusinessLogic.Interfaces;
using HeavenlyKingdom.DataAccess.Interfaces;
using HeavenlyKingdom.Domain.DTOs;
using HeavenlyKingdom.Domain.Entities;

namespace HeavenlyKingdom.BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepo, IProductRepository productRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<List<OrderDto>> GetActiveAsync(int userId)
        {
            var orders = await _orderRepo.GetActiveByUserIdAsync(userId);
            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<List<OrderDto>> GetHistoryAsync(int userId)
        {
            var orders = await _orderRepo.GetHistoryByUserIdAsync(userId);
            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var order = await _orderRepo.GetByIdAsync(id);
            return order == null ? null : _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateAsync(int userId, CreateOrderDto dto)
        {
            var items = new List<OrderItem>();
            decimal total = 0;

            foreach (var itemDto in dto.Items)
            {
                var product = await _productRepo.GetByIdAsync(itemDto.ProductId);
                if (product == null) continue;

                items.Add(new OrderItem
                {
                    ProductId = itemDto.ProductId,
                    Quantity = itemDto.Quantity,
                    Price = product.Price
                });

                total += product.Price * itemDto.Quantity;
            }

            var order = new Order
            {
                UserId = userId,
                AddressId = dto.AddressId,
                CreatedAt = DateTime.UtcNow,
                Status = "placed",
                TotalAmount = total,
                Items = items
            };

            await _orderRepo.AddAsync(order);
            
            // Получаем заказ с подгруженными данными (Eager Loading) для корректного маппинга
            var fullOrder = await _orderRepo.GetByIdAsync(order.Id);
            return _mapper.Map<OrderDto>(fullOrder!);
        }
    }
}