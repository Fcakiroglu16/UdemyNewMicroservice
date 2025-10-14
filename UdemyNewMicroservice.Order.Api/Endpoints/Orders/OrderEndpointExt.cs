#region

using Asp.Versioning.Builder;

#endregion

namespace UdemyNewMicroservice.Order.Api.Endpoints.Orders;

public static class OrderEndpointExt
{
    public static void AddOrderGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        app.MapGroup("api/v{version:apiVersion}/orders").WithTags("Orders")
            .WithApiVersionSet(apiVersionSet)
            .CreateOrderGroupItemEndpoint()
            .GetOrdersGroupItemEndpoint().RequireAuthorization("Password");
    }
}