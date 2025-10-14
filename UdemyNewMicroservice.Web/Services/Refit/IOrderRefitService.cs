#region

using Refit;
using UdemyNewMicroservice.Web.Pages.Order.Dto;

#endregion

namespace UdemyNewMicroservice.Web.Services.Refit;

public interface IOrderRefitService
{
    //CreateOrder endpoint
    [Post("/api/v1/orders")]
    Task<ApiResponse<object>> CreateOrder(CreateOrderRequest request);

    [Get("/api/v1/orders")]
    Task<ApiResponse<List<GetOrderHistoryResponse>>> GetOrders();
}