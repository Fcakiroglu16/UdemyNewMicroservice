using Asp.Versioning.Builder;
using UdemyNewMicroservice.Order.Api.Endpoints.Orders;

namespace UdemyNewMicroservice.Catalog.Api.Features.Categories;

public static class OrderEndpointExt
{
    public static void AddOrderGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        app.MapGroup("api/v{version:apiVersion}/orders").WithTags("Orders")
            .WithApiVersionSet(apiVersionSet)
            .CreateOrderGroupItemEndpoint()
            .GetOrdersGroupItemEndpoint();
    }
}