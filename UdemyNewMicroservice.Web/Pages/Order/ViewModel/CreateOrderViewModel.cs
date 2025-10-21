#region

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using UdemyNewMicroservice.Web.Pages.Basket.ViewModel;

#endregion

namespace UdemyNewMicroservice.Web.Pages.Order.ViewModel;

public record CreateOrderViewModel
{
    public AddressViewModel Address { get; set; } = null!;

    public PaymentViewModel Payment { get; set; } = null!;

    [ValidateNever] public List<OrderItemViewModel> OrderItems { get; set; } = [];


    [ValidateNever] public float? DiscountRate { get; set; }


    public decimal TotalPrice { get; set; }

    public static CreateOrderViewModel Empty => new()
    {
        Address = AddressViewModel.Empty,
        Payment = PaymentViewModel.Empty
    };


    public void AddOrderItem(BasketItemViewModel basketItem)
    {
        OrderItems.Add(new OrderItemViewModel(basketItem.Id, basketItem.Name,
            basketItem.PriceByApplyDiscountRate ?? basketItem.Price));
    }
}