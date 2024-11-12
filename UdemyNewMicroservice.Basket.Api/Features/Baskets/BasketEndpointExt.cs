﻿using Asp.Versioning.Builder;
using UdemyNewMicroservice.Basket.Api.Features.Baskets.AddBasketItem;
using UdemyNewMicroservice.Basket.Api.Features.Baskets.DeleteBasketItem;
using UdemyNewMicroservice.Basket.Api.Features.Baskets.GetBasket;

namespace UdemyNewMicroservice.Basket.Api.Features.Baskets
{
    public static class BasketEndpointExt
    {
        public static void AddBasketGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/baskets").WithTags("Baskets")
                .WithApiVersionSet(apiVersionSet)
                .AddBasketItemGroupItemEndpoint()
                .DeleteBasketItemGroupItemEndpoint()
                .GetBasketGroupItemEndpoint();
        }
    }
}