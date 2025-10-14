#region

using Refit;
using UdemyNewMicroservice.Web.Pages.Basket.Dto;

#endregion

namespace UdemyNewMicroservice.Web.Services.Refit;

public interface IBasketRefitService
{
    [Post("/api/v1/baskets/item")]
    Task<ApiResponse<object>> AddBasketItemAsync(AddBasketRequest request);

    [Get("/api/v1/baskets/user")]
    Task<ApiResponse<BasketResponse>> GetBasketsAsync();


    [Put("/api/v1/baskets/apply-discount-coupon")]
    Task<ApiResponse<object>> ApplyDiscountRateAsync(ApplyDiscountRateRequest applyDiscountRateRequest);


    [Delete("/api/v1/baskets/remove-discount-coupon")]
    Task<ApiResponse<object>> RemoveDiscountRateAsync();


    [Delete("/api/v1/basket/{courseId}")]
    Task<ApiResponse<object>> DeleteItemAsync(Guid courseId);
}