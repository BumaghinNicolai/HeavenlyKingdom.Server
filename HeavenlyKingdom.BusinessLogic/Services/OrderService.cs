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
            return orders.Select(MapToDto).ToList();
        }

        public async Task<List<OrderDto>> GetHistoryAsync(int userId)
        {
            var orders = await _orderRepo.GetHistoryByUserIdAsync(userId);
            return orders.Select(MapToDto).ToList();
        }

        public async Task<OrderDto?> GetByIdAsync(int id)
        {
            var order = await _orderRepo.GetByIdAsync(id);
            return order == null ? null : MapToDto(order);
        }

        public async Task<OrderDto> CreateAsync(int userId, CreateOrderDto dto)
        {
            var items = new List<OrderItem>();
            decimal total = 0;

            foreach (var item in dto.Items)
            {
                var product = await _productRepo.GetByIdAsync(item.ProductId);
                if (product == null) continue;

                items.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = product.Price
                });

                total += product.Price * item.Quantity;
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

            var created = await _orderRepo.AddAsync(order);
            var full = await _orderRepo.GetByIdAsync(created.Id);
            return MapToDto(full!);
        }

        private static OrderDto MapToDto(Order o) => new()
        {
            Id = o.Id,
            Number = $"ORD-{o.Id:D5}",
            CreatedAt = o.CreatedAt,
            Status = o.Status,
            TotalAmount = o.TotalAmount,
            Items = o.Items.Select(i => new OrderItemDto
            {
                Id = i.Id,
                ProductName = i.Product?.Name ?? string.Empty,
                ProductImg = i.Product?.Img ?? string.Empty,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList()
        };
    }
}
