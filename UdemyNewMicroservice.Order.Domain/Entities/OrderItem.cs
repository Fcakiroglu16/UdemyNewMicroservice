namespace UdemyNewMicroservice.Order.Domain.Entities;

// Anemic  Model => Rich Domain Model
public class OrderItem : BaseEntity<int>
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal UnitPrice { get; set; }

    public Guid OrderId { get; set; }

    public Order Order { get; set; } = null!;

    public void SetItem(Guid productId, string productName, decimal unitPrice)
    {
        if (string.IsNullOrEmpty(productName)) throw new ArgumentNullException(nameof(productName), "ProductName cannot be empty");

        if (unitPrice <= 0) throw new ArgumentNullException(nameof(unitPrice), "UnitPrice cannot be less than or equal to zero");


        ProductId = productId;
        ProductName = productName;
        UnitPrice = unitPrice;
    }

    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice <= 0) throw new ArgumentNullException("UnitPrice cannot be less than or equal to zero");
        UnitPrice = newPrice;
    }

    public void ApplyDiscount(float discountPercentage)
    {
        if (discountPercentage < 0 || discountPercentage > 100) throw new ArgumentNullException("Discount percentage must be between 0 and 100");
        UnitPrice -= UnitPrice * (decimal)discountPercentage / 100;
    }


    public bool IsSameItem(OrderItem otherItem)
    {
        return ProductId == otherItem.ProductId;
    }
}