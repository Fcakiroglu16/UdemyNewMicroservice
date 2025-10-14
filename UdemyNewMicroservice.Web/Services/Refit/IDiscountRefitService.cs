#region

using Refit;
using UdemyNewMicroservice.Web.Pages.Basket.Dto;

#endregion

namespace UdemyNewMicroservice.Web.Services.Refit;

public interface IDiscountRefitService
{
    [Get("/api/v1/discounts/{coupon}")]
    Task<ApiResponse<GetDiscountByCouponResponse>> GetDiscountByCoupon(string coupon);
}