namespace UdemyNewMicroservice.Order.Domain.Entities
{
    // Anemic  Model => Rich Domain Model
    public class OrderItem:BaseEntity<int>
    {

        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal UnitPrice { get; set; }

        public Guid  OrderId { get; set; }

        public Order Order { get; set; } = null!;

        public void SetItem(Guid productId, string productName, decimal unitPrice)
        {

            if (string.IsNullOrEmpty(ProductName))
            {
                throw new ArgumentNullException(nameof(productName),"ProductName cannot be empty");
            }

            if (UnitPrice <= 0)
            {
                throw new ArgumentNullException(nameof(unitPrice),"UnitPrice cannot be less than or equal to zero");
            }


            this.ProductId = productId;
            this.ProductName = productName;
            this.UnitPrice = unitPrice;
        }

        public void UpdatePrice(decimal newPrice)
        {

            if (newPrice <= 0)
            {
                throw new ArgumentNullException("UnitPrice cannot be less than or equal to zero");
            }
            this.UnitPrice = newPrice;
        }

        public void ApplyDiscount(float discountPercentage)
        {
            if (discountPercentage < 0 || discountPercentage > 100)
            {
                throw new ArgumentNullException("Discount percentage must be between 0 and 100");
            }
            this.UnitPrice -= (this.UnitPrice * (decimal)discountPercentage / 100);
        }


        public bool IsSameItem(OrderItem otherItem)
        {

            return this.ProductId == otherItem.ProductId;
        }





    }
}
