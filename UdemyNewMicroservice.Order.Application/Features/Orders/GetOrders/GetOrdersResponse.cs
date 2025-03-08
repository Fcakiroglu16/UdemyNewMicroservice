using UdemyNewMicroservice.Order.Application.Features.Orders.CreateOrder;

namespace UdemyNewMicroservice.Order.Application.Features.Orders.GetOrders;

public record GetOrdersResponse(DateTime Created, decimal TotalPrice, List<OrderItemDto> Items);