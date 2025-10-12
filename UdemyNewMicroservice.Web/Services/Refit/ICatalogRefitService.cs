#region

using Refit;
using UdemyNewMicroservice.Web.Pages.Instructor.Dto;

#endregion

namespace UdemyNewMicroservice.Web.Services.Refit;

public interface ICatalogRefitService
{
    [Get("/api/v1/categories")]
    Task<ApiResponse<List<CategoryDto>>> GetCategoriesAsync();

    [Multipart]
    [Post("/api/v1/courses")]
    Task<ApiResponse<object>> CreateCourseAsync(
        [AliasAs("Name")] string Name,
        [AliasAs("Description")] string Description,
        [AliasAs("Price")] decimal Price,
        [AliasAs("Picture")] StreamPart? Picture,
        [AliasAs("CategoryId")] string CategoryId);


    [Put("/api/v1/courses")]
    Task<ApiResponse<object>> UpdateCourseAsync(UpdateCourseRequest request);


    [Delete("/api/v1/courses/{id}")]
    Task<ApiResponse<object>> DeleteCourseAsync(Guid id);
}