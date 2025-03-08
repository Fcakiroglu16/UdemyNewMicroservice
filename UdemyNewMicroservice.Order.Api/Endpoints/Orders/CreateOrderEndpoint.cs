using MediatR;
using Microsoft.AspNetCore.Mvc;
using UdemyNewMicroservice.Order.Application.Features.Orders.Create;
using UdemyNewMicroservice.Shared.Extensions;
using UdemyNewMicroservice.Shared.Filters;

namespace UdemyNewMicroservice.Order.Api.Endpoints.Orders;

public static class CreateOrderEndpoint
{
    public static RouteGroupBuilder CreateOrderGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/",
                async ([FromBody] CreateOrderCommand command, [FromServices] IMediator mediator) =>
                (await mediator.Send(command)).ToGenericResult())
            .WithName("CreateOrder")
            .MapToApiVersion(1, 0)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .AddEndpointFilter<ValidationFilter<CreateOrderCommandValidator>>();

        return group;
    }
}