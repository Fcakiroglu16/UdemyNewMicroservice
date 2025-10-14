#region

using UdemyNewMicroservice.Basket.Api.Dto;
using UdemyNewMicroservice.Shared;

#endregion

namespace UdemyNewMicroservice.Basket.Api.Features.Baskets.GetBasket;

public record GetBasketQuery : IRequestByServiceResult<BasketDto>;