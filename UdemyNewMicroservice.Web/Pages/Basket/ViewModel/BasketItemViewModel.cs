namespace UdemyNewMicroservice.Web.Pages.Basket.ViewModel;

public record BasketItemViewModel(
    Guid Id,
    string Name,
    string ImageUrl,
    decimal Price,
    decimal? PriceByApplyDiscountRate)
{
}