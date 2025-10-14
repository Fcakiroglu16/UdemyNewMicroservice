namespace UdemyNewMicroservice.Web.Pages.Basket.ViewModel;

public record BasketViewModel(
    float? DiscountRate,
    string? Coupon,
    decimal TotalPrice,
    decimal? TotalPriceWithAppliedDiscount,
    List<BasketItemViewModel> Items
)
{
    public static BasketViewModel Empty()
    {
        return new BasketViewModel(0, string.Empty, 0, 0, []);
    }
}