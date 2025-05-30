using AutoMapper;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Aggregates;
using ECommerce.Domain.Entities;

namespace ECommerce.Application.Mappings
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Price.Amount))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Price.Currency));

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice.Amount))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.TotalPrice.Currency))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus.ToString()))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems));
        }
    }
}
