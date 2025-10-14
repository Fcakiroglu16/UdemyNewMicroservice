#region

using AutoMapper;
using UdemyNewMicroservice.Basket.Api.Data;
using UdemyNewMicroservice.Basket.Api.Dto;

#endregion

namespace UdemyNewMicroservice.Basket.Api.Features.Baskets;

public class BasketMapping : Profile
{
    public BasketMapping()
    {
        CreateMap<BasketDto, Data.Basket>().ReverseMap();
        CreateMap<BasketItemDto, BasketItem>().ReverseMap();
    }
}