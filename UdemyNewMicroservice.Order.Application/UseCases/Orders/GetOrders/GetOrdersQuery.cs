#region

using UdemyNewMicroservice.Shared;

#endregion

namespace UdemyNewMicroservice.Order.Application.UseCases.Orders.GetOrders;

public record GetOrdersQuery : IRequestByServiceResult<List<GetOrdersResponse>>;