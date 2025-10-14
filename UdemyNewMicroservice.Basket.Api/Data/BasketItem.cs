namespace UdemyNewMicroservice.Basket.Api.Data;

public class BasketItem
{
    public BasketItem(Guid id, string name, string? imageUrl, decimal price, decimal? priceByApplyDiscountRate)
    {
        Id = id;
        Name = name;
        ImageUrl = imageUrl;
        Price = price;
        PriceByApplyDiscountRate = priceByApplyDiscountRate;
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? ImageUrl { get; set; }
    public decimal Price { get; set; }
    public decimal? PriceByApplyDiscountRate { get; set; }
}