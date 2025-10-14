#region

using UdemyNewMicroservice.Shared;

#endregion

namespace UdemyNewMicroservice.Order.Application.UseCases.Orders.CreateOrder;

public record CreateOrderCommand(float? DiscountRate, AddressDto Address, PaymentDto Payment, List<OrderItemDto> Items)
    : IRequestByServiceResult;

public record AddressDto(string Province, string District, string Street, string ZipCode, string Line);

public record PaymentDto(string CardNumber, string CardHolderName, string Expiration, string Cvc, decimal Amount);