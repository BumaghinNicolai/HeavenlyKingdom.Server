using AutoMapper;
using HeavenlyKingdom.Domain.DTOs;
using HeavenlyKingdom.Domain.Entities;

namespace HeavenlyKingdom.Helpers.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            // Product — Cat берём из Category.Name
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.Cat, o => o.MapFrom(s => s.Category.Name));
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();

            // CartItem → плоский DTO под фронт
            // img, name, cat, price тянем из вложенного Product
            CreateMap<CartItem, CartItemDto>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Product.Name))
                .ForMember(d => d.Cat, o => o.MapFrom(s => s.Product.Category.Name))
                .ForMember(d => d.Img, o => o.MapFrom(s => s.Product.Img))
                .ForMember(d => d.Price, o => o.MapFrom(s => s.Product.Price))
                .ForMember(d => d.Qty, o => o.MapFrom(s => s.Quantity));

            CreateMap<AddToCartDto, CartItem>()
                .ForMember(d => d.Quantity, o => o.MapFrom(s => s.Qty));

            // User
            CreateMap<User, UserResponseDto>();

            // Father
            CreateMap<Father, FatherDto>();
            CreateMap<CreateFatherDto, Father>();
            CreateMap<UpdateFatherDto, Father>();
            
            // Candle
            CreateMap<Candle, CandleDto>();
            CreateMap<CreateCandleDto, Candle>();
            CreateMap<UpdateCandleDto, Candle>();

            // Address
            CreateMap<Address, AddressDto>();

            // Notification
            CreateMap<Notification, NotificationDto>();

            // ChapelCandle
            CreateMap<ChapelCandle, ChapelCandleDto>();

            // Favorite
            CreateMap<Favorite, FavoriteDto>()
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.Name))
                .ForMember(d => d.ProductImg, o => o.MapFrom(s => s.Product.Img))
                .ForMember(d => d.ProductPrice, o => o.MapFrom(s => s.Product.Price))
                .ForMember(d => d.ProductCat, o => o.MapFrom(s => s.Product.Category.Name));
        }
    }
}