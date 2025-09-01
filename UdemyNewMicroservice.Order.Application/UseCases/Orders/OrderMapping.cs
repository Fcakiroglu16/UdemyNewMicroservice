using AutoMapper;
using UdemyNewMicroservice.Order.Application.UseCases.Orders.CreateOrder;
using UdemyNewMicroservice.Order.Domain.Entities;

namespace UdemyNewMicroservice.Order.Application.UseCases.Orders;

public class OrderMapping : Profile
{
    public OrderMapping()
    {
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
    }
}