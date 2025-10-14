#region

using UdemyNewMicroservice.Shared;

#endregion

namespace UdemyNewMicroservice.Basket.Api.Features.Baskets.DeleteBasketItem;

public record DeleteBasketItemCommand(Guid Id) : IRequestByServiceResult;