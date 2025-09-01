using UdemyNewMicroservice.Shared;

namespace UdemyNewMicroservice.Order.Application.UseCases.Orders.GetOrders;

public record GetOrdersQuery : IRequestByServiceResult<List<GetOrdersResponse>>;