using AutoMapper;
using InternshipTradingApp.OrderManagementSystem.DTOs;
using InternshipTradingApp.OrderManagementSystem.Entities;

public class OrderMapperProfile : Profile
{
    public OrderMapperProfile()
    {
        CreateMap<Order, OrderDetailsDTO>();

        CreateMap<CreateOrderDTO, Order>()
            .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => DateTime.Now)) 
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => OrderStatus.Pending)); 
    }
}
