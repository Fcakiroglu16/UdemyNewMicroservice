#region

using Asp.Versioning.Builder;
using UdemyNewMicroservice.Catalog.Api.Features.Categories.Create;
using UdemyNewMicroservice.Catalog.Api.Features.Categories.GetAll;
using UdemyNewMicroservice.Catalog.Api.Features.Categories.GetById;

#endregion

namespace UdemyNewMicroservice.Catalog.Api.Features.Categories;

public static class CategoryEndpointExt
{
    public static void AddCategoryGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        app.MapGroup("api/v{version:apiVersion}/categories").WithTags("Categories")
            .WithApiVersionSet(apiVersionSet)
            .CreateCategoryGroupItemEndpoint()
            .GetAllCategoryGroupItemEndpoint()
            .GetByIdCategoryGroupItemEndpoint();
    }
}