#region

using UdemyNewMicroservice.Shared;

#endregion

namespace UdemyNewMicroservice.Basket.Api.Features.Baskets.ApplyDiscountCoupon;

public record ApplyDiscountCouponCommand(string Coupon, float DiscountRate) : IRequestByServiceResult;