#region

using System.Text.Json.Serialization;

#endregion

namespace UdemyNewMicroservice.Basket.Api.Dto;

public record BasketDto
{
    public BasketDto(List<BasketItemDto> items)
    {
        Items = items;
    }

    public BasketDto()
    {
    }

    [JsonIgnore] public bool IsApplyDiscount => DiscountRate is > 0 && !string.IsNullOrEmpty(Coupon);

    public List<BasketItemDto> Items { get; set; } = new();

    public float? DiscountRate { get; set; }
    public string? Coupon { get; set; }


    public decimal TotalPrice => Items.Sum(x => x.Price);


    public decimal? TotalPriceWithAppliedDiscount =>
        !IsApplyDiscount ? null : Items.Sum(x => x.PriceByApplyDiscountRate);
}