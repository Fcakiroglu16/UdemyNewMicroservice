#region

using Microsoft.AspNetCore.Mvc;

#endregion

namespace UdemyNewMicroservice.Discount.Api.Features.Discounts.GetDiscountByCode;

public static class GetDiscountByCodeEndpoint
{
    public static RouteGroupBuilder GetDiscountByCodeGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/{code:length(10)}",
                async (string code, IMediator mediator) =>
                    (await mediator.Send(new GetDiscountByCodeQuery(code))).ToGenericResult())
            .WithName("GetDiscountByCode")
            .MapToApiVersion(1, 0)
            .Produces<GetDiscountByCodeQueryResponse>()
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .RequireAuthorization(policyNames: "Password");

        return group;
    }
}