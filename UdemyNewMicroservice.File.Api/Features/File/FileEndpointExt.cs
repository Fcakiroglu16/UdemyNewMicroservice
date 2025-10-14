#region

using Asp.Versioning.Builder;
using UdemyNewMicroservice.Discount.Api.Features.Discounts.CreateDiscount;
using UdemyNewMicroservice.File.Api.Features.File.Delete;

#endregion

namespace UdemyNewMicroservice.File.Api.Features.File;

public static class FileEndpointExt
{
    public static void AddFileGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        app.MapGroup("api/v{version:apiVersion}/files").WithTags("files").WithApiVersionSet(apiVersionSet)
            .UploadFileGroupItemEndpoint().DeleteFileGroupItemEndpoint().RequireAuthorization();
    }
}