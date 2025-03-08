using AutoMapper;
using MediatR;
using UdemyNewMicroservice.Order.Application.Contracts.Repositories;
using UdemyNewMicroservice.Order.Application.Features.Orders.CreateOrder;
using UdemyNewMicroservice.Shared;
using UdemyNewMicroservice.Shared.Services;

namespace UdemyNewMicroservice.Order.Application.Features.Orders.GetOrders;

public class GetOrdersQueryHandler(IIdentityService identityService, IOrderRepository orderRepository, IMapper mapper) : IRequestHandler<GetOrdersQuery, ServiceResult<List<GetOrdersResponse>>>
{
    public async Task<ServiceResult<List<GetOrdersResponse>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetOrderByBuyerId(identityService.GetUserId);


        var response = orders.Select(o => new GetOrdersResponse(o.Created, o.TotalPrice, mapper.Map<List<OrderItemDto>>(o.OrderItems))).ToList();


        return ServiceResult<List<GetOrdersResponse>>.SuccessAsOk(response);
    }
}