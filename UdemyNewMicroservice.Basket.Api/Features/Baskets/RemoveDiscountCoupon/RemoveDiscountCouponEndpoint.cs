#region

using System.Net;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using UdemyNewMicroservice.Shared;
using UdemyNewMicroservice.Shared.Extensions;
using UdemyNewMicroservice.Shared.Services;

#endregion

namespace UdemyNewMicroservice.Basket.Api.Features.Baskets.RemoveDiscountCoupon;

public record RemoveDiscountCouponCommand : IRequestByServiceResult;

public class RemoveDiscountCouponCommandHandler(
    IIdentityService identityService,
    IDistributedCache distributedCache,
    BasketService basketService) : IRequestHandler<RemoveDiscountCouponCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(RemoveDiscountCouponCommand request,
        CancellationToken cancellationToken)
    {
        var basketAsJson = await basketService.GetBasketFromCache(cancellationToken);

        if (string.IsNullOrEmpty(basketAsJson)) return ServiceResult.Error("Basket not found", HttpStatusCode.NotFound);

        var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);

        basket!.ClearDiscount();


        basketAsJson = JsonSerializer.Serialize(basket);

        await basketService.CreateBasketCacheAsync(basket, cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}

public static class RemoveDiscountCouponEndpoint
{
    public static RouteGroupBuilder RemoveDiscountCouponGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapDelete("/remove-discount-coupon",
                async (IMediator mediator) =>
                    (await mediator.Send(new RemoveDiscountCouponCommand())).ToGenericResult())
            .WithName("RemoveDiscountCoupon")
            .MapToApiVersion(1, 0);


        return group;
    }
}