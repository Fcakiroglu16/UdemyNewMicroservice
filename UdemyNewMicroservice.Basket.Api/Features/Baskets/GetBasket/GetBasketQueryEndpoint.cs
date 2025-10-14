#region

using MediatR;
using UdemyNewMicroservice.Shared.Extensions;

#endregion

namespace UdemyNewMicroservice.Basket.Api.Features.Baskets.GetBasket;

public static class GetBasketQueryEndpoint
{
    public static RouteGroupBuilder GetBasketGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/user",
                async (IMediator mediator) =>
                    (await mediator.Send(new GetBasketQuery())).ToGenericResult())
            .WithName("GetBasket")
            .MapToApiVersion(1, 0);


        return group;
    }
}