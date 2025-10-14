#region

using UdemyNewMicroservice.Web.Extensions;
using UdemyNewMicroservice.Web.Pages.Basket.Dto;
using UdemyNewMicroservice.Web.Services.Refit;

#endregion

namespace UdemyNewMicroservice.Web.Services;

public class BasketService(
    IBasketRefitService basketRefitService,
    IDiscountRefitService discountRefitService,
    ILogger<BasketService> logger)
{
    public async Task<ServiceResult> CreateOrUpdateBasketAsync(AddBasketRequest request)
    {
        var responseAsResult = await basketRefitService.AddBasketItemAsync(request);

        if (!responseAsResult.IsSuccessStatusCode)
        {
            logger.LogProblemDetails(responseAsResult.Error);
            return ServiceResult.Error("An error occurred while creating or updating the basket");
        }


        return ServiceResult.Success();
    }


    public async Task<ServiceResult<BasketResponse>> GetBasketsAsync()
    {
        var responseAsResult = await basketRefitService.GetBasketsAsync();

        if (!responseAsResult.IsSuccessStatusCode)
        {
            logger.LogProblemDetails(responseAsResult.Error);
            return ServiceResult<BasketResponse>.Error("An error occurred while getting the baskets");
        }


        return ServiceResult<BasketResponse>.Success(responseAsResult.Content!);
    }

    public async Task<ServiceResult> DeleteBasketAsync(Guid courseId)
    {
        var responseAsResult = await basketRefitService.DeleteItemAsync(courseId);

        if (!responseAsResult.IsSuccessStatusCode)
        {
            logger.LogProblemDetails(responseAsResult.Error);
            return ServiceResult.Error("An error occurred while deleting the basket");
        }

        return ServiceResult.Success();
    }

    public async Task<ServiceResult> ApplyDiscountAsync(string coupon)
    {
        var responseAsResult = await discountRefitService.GetDiscountByCoupon(coupon);

        if (!responseAsResult.IsSuccessStatusCode) return ServiceResult.FailFromProblemDetails(responseAsResult.Error);


        var discount = responseAsResult.Content;

        var response =
            await basketRefitService.ApplyDiscountRateAsync(new ApplyDiscountRateRequest(coupon,
                responseAsResult.Content!.Rate));
        if (!responseAsResult.IsSuccessStatusCode)
        {
            logger.LogProblemDetails(responseAsResult.Error);
            return ServiceResult.Error("An error occurred while applying the discount");
        }


        return ServiceResult.Success();
    }


    public async Task<ServiceResult> RemoveDiscountAsync()
    {
        var response = await basketRefitService.RemoveDiscountRateAsync();
        if (!response.IsSuccessStatusCode)
        {
            logger.LogProblemDetails(response.Error);
            return ServiceResult.Error("An error occurred while removing the discount");
        }

        return ServiceResult.Success();
    }
}