namespace UdemyNewMicroservice.Web.Pages.Basket.Dto;

public record BasketItemDto(
    Guid Id,
    string Name,
    string ImageUrl,
    decimal Price,
    decimal? PriceByApplyDiscountRate)
{
}