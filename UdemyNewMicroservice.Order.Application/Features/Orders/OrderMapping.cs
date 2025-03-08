using AutoMapper;
using UdemyNewMicroservice.Order.Application.Features.Orders.CreateOrder;
using UdemyNewMicroservice.Order.Domain.Entities;

namespace UdemyNewMicroservice.Order.Application.Features.Orders;

public class OrderMapping : Profile
{
    public OrderMapping()
    {
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
    }
}