#region

using MediatR;
using Microsoft.AspNetCore.Mvc;
using UdemyNewMicroservice.File.Api.Features.File.Upload;
using UdemyNewMicroservice.Shared.Extensions;

#endregion

namespace UdemyNewMicroservice.Discount.Api.Features.Discounts.CreateDiscount;

public static class UploadFileCommandEndpoint
{
    public static RouteGroupBuilder UploadFileGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapPost("/",
                async (IFormFile file, IMediator mediator) =>
                    (await mediator.Send(new UploadFileCommand(file))).ToGenericResult())
            .WithName("upload")
            .MapToApiVersion(1, 0)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError).DisableAntiforgery();

        return group;
    }
}