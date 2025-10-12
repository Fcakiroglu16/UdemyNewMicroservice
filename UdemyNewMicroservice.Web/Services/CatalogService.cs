#region

using System.Text.Json;
using Refit;
using UdemyNewMicroservice.Web.Pages.Instructor.ViewModel;
using UdemyNewMicroservice.Web.Services.Refit;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

#endregion

namespace UdemyNewMicroservice.Web.Services;

public class CatalogService(ICatalogRefitService catalogRefitService, ILogger<CatalogService> logger)
{
    public async Task<ServiceResult<List<CategoryViewModel>>> GetCategoriesAsync()
    {
        var response = await catalogRefitService.GetCategoriesAsync();
        if (!response.IsSuccessStatusCode)
        {
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response.Error.Content!);
            logger.LogError("Error occurred while fetching categories");
            return ServiceResult<List<CategoryViewModel>>.Error("Fail to retrieve categories. Please try again later");
        }

        var categories = response!.Content!
            .Select(c => new CategoryViewModel(c.Id, c.Name))
            .ToList();
        return ServiceResult<List<CategoryViewModel>>.Success(categories);
    }

    public async Task<ServiceResult> CreateCourseAsync(CreateCourseViewModel model)
    {
        StreamPart? pictureStreamPart = null;
        await using var stream = model.PictureFormFile?.OpenReadStream();

        if (model.PictureFormFile is not null && model.PictureFormFile.Length > 0)
            pictureStreamPart =
                new StreamPart(stream!, model.PictureFormFile.FileName, model.PictureFormFile.ContentType);


        var response = await catalogRefitService.CreateCourseAsync(
            model.Name,
            model.Description,
            model.Price,
            pictureStreamPart,
            model.CategoryId.ToString()!
        );

        if (!response.IsSuccessStatusCode)
        {
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response.Error.Content!);
            logger.LogError("Error occurred while creating course");
            return ServiceResult.Error("Fail to create course. Please try again later");
        }


        return ServiceResult.Success();
    }
}