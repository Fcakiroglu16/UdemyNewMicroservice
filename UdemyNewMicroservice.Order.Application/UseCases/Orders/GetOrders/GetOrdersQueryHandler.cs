#region

using AutoMapper;
using MediatR;
using UdemyNewMicroservice.Order.Application.Contracts.Repositories;
using UdemyNewMicroservice.Order.Application.UseCases.Orders.CreateOrder;
using UdemyNewMicroservice.Shared;
using UdemyNewMicroservice.Shared.Services;

#endregion

namespace UdemyNewMicroservice.Order.Application.UseCases.Orders.GetOrders;

public class GetOrdersQueryHandler(IIdentityService identityService, IOrderRepository orderRepository, IMapper mapper)
    : IRequestHandler<GetOrdersQuery, ServiceResult<List<GetOrdersResponse>>>
{
    public async Task<ServiceResult<List<GetOrdersResponse>>> Handle(GetOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetOrderByBuyerId(identityService.UserId);


        var response = orders.Select(o =>
            new GetOrdersResponse(o.Created, o.TotalPrice, mapper.Map<List<OrderItemDto>>(o.OrderItems))).ToList();


        return ServiceResult<List<GetOrdersResponse>>.SuccessAsOk(response);
    }
}