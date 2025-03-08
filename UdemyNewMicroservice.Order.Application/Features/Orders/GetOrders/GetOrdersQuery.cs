using UdemyNewMicroservice.Shared;

namespace UdemyNewMicroservice.Order.Application.Features.Orders.GetOrders;

public record GetOrdersQuery : IRequestByServiceResult<List<GetOrdersResponse>>;