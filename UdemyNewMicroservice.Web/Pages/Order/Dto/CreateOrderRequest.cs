namespace UdemyNewMicroservice.Web.Pages.Order.Dto;

public record CreateOrderRequest(
    float? DiscountRate,
    AddressDto Address,
    PaymentDto Payment,
    List<OrderItemDto> Items);