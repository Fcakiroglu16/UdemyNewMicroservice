#region

using MediatR;
using UdemyNewMicroservice.Shared.Extensions;
using UdemyNewMicroservice.Shared.Filters;

#endregion

namespace UdemyNewMicroservice.Basket.Api.Features.Baskets.ApplyDiscountCoupon;

public static class ApplyDiscountCouponEndpoint
{
    public static RouteGroupBuilder ApplyDiscountCouponGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPut("/apply-discount-coupon",
                async (ApplyDiscountCouponCommand command, IMediator mediator) =>
                    (await mediator.Send(command)).ToGenericResult())
            .WithName("ApplyDiscountCoupon")
            .MapToApiVersion(1, 0)
            .AddEndpointFilter<ValidationFilter<ApplyDiscountCouponCommand>>();
        return group;
    }
}