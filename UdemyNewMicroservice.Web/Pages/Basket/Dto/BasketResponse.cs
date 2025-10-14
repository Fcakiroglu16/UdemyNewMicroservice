namespace UdemyNewMicroservice.Web.Pages.Basket.Dto;

public record BasketResponse(
    float? DiscountRate,
    string? Coupon,
    decimal TotalPrice,
    decimal? TotalPriceWithAppliedDiscount,
    List<BasketItemDto> Items
);