#region

using System.Net;
using UdemyNewMicroservice.Web.Extensions;
using UdemyNewMicroservice.Web.Pages.Basket.Dto;
using UdemyNewMicroservice.Web.Pages.Basket.ViewModel;
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


    public async Task<ServiceResult<BasketViewModel>> GetBasketsAsync()
    {
        var responseAsResult = await basketRefitService.GetBasketsAsync();

        if (!responseAsResult.IsSuccessStatusCode)
        {
            if (responseAsResult.StatusCode == HttpStatusCode.NotFound)
                return ServiceResult<BasketViewModel>.Success(BasketViewModel.Empty());


            logger.LogProblemDetails(responseAsResult.Error);
            return ServiceResult<BasketViewModel>.Error("An error occurred while getting the baskets");
        }


        var basketViewModel = new BasketViewModel(
            responseAsResult.Content!.DiscountRate,
            responseAsResult.Content.Coupon,
            responseAsResult.Content.TotalPrice,
            responseAsResult.Content.TotalPriceWithAppliedDiscount,
            responseAsResult.Content.Items.Select(item => new BasketItemViewModel(
                item.Id,
                item.Name,
                item.ImageUrl, item.Price,
                item.PriceByApplyDiscountRate
            )).ToList()
        );

        return ServiceResult<BasketViewModel>.Success(basketViewModel);
    }


    public async Task<ServiceResult<BasketPageViewModel>> GetBasketPageViewModelAsync()
    {
        var basketsAsResult = await GetBasketsAsync();

        if (basketsAsResult.IsFail)
            return ServiceResult<BasketPageViewModel>.Error(basketsAsResult.Fail!);

        var basketPageViewModel = new BasketPageViewModel();


        basketPageViewModel.SetPrice(basketsAsResult.Data!.TotalPrice,
            basketsAsResult.Data.TotalPriceWithAppliedDiscount);
        basketPageViewModel.DiscountRate = basketsAsResult.Data.DiscountRate;
        basketPageViewModel.Coupon = basketsAsResult.Data.Coupon;


        foreach (var basketItem in basketsAsResult.Data!.Items)
            basketPageViewModel.Items.Add(new BasketViewModelItem(basketItem.Id, basketItem.ImageUrl,
                basketItem.Name,
                basketItem.Price, basketItem.PriceByApplyDiscountRate));


        return ServiceResult<BasketPageViewModel>.Success(basketPageViewModel);
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