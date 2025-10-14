#region

using UdemyNewMicroservice.Order.Application.UseCases.Orders.CreateOrder;

#endregion

namespace UdemyNewMicroservice.Order.Application.UseCases.Orders.GetOrders;

public record GetOrdersResponse(DateTime Created, decimal TotalPrice, List<OrderItemDto> Items);