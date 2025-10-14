#region

using System.Text.Json;
using Refit;
using UdemyNewMicroservice.Web.Extensions;
using UdemyNewMicroservice.Web.Services.Refit;
using UdemyNewMicroservice.Web.ViewModel;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

#endregion

namespace UdemyNewMicroservice.Web.Services;

public class CatalogService(
    ICatalogRefitService catalogRefitService,
    UserService userService,
    ILogger<CatalogService> logger)
{
    public async Task<ServiceResult<List<CourseViewModel>>> GetAllCoursesAsync()
    {
        var coursesAsResult = await catalogRefitService.GetAllCourses();

        if (!coursesAsResult.IsSuccessStatusCode)
        {
            logger.LogProblemDetails(coursesAsResult.Error);

            return ServiceResult<List<CourseViewModel>>.Error(
                "Failed to retrieve course data. Please try again later.");
        }


        var courses = coursesAsResult.Content!;

        var categoriesViewModel = courses.Select(c =>
            new CourseViewModel(c.Id, c.Name, c.Description, c.Price, c.ImageUrl, c.Created.ToLongDateString(),
                c.Feature.EducatorFullName, c.Category.Name, c.Feature.Duration,
                c.Feature.Rating)).ToList();

        return ServiceResult<List<CourseViewModel>>.Success(categoriesViewModel);
    }


    public async Task<ServiceResult<CourseViewModel>> GetCourse(Guid courseId)
    {
        var response = await catalogRefitService.GetCourse(courseId);

        if (!response.IsSuccessStatusCode)
            return ServiceResult<CourseViewModel>.FailFromProblemDetails(response.Error);


        var course = response.Content!;
        var courseViewModel = new CourseViewModel(course.Id, course.Name, course.Description, course.Price,
            course.ImageUrl, course.Created.ToLongDateString(), course.Feature.EducatorFullName, course.Category.Name,
            course.Feature.Duration,
            course.Feature.Rating);

        return ServiceResult<CourseViewModel>.Success(courseViewModel);
    }


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


    public async Task<ServiceResult<List<CourseViewModel>>> GetCoursesByUserId()
    {
        var course = await catalogRefitService.GetCoursesByUserId(userService.UserId);

        if (!course.IsSuccessStatusCode)
        {
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(course.Error.Content!);
            logger.LogError("Error occurred while fetching courses by user id");
            return ServiceResult<List<CourseViewModel>>.Error("Fail to retrieve courses. Please try again later");
        }

        var courses = course!.Content!
            .Select(c => new CourseViewModel(
                c.Id,
                c.Name,
                c.Description,
                c.Price,
                c.ImageUrl,
                c.Created.ToLongDateString(),
                c.Feature.EducatorFullName,
                c.Category.Name,
                c.Feature.Duration,
                c.Feature.Rating
            ))
            .ToList();

        return ServiceResult<List<CourseViewModel>>.Success(courses);
    }

    public async Task<ServiceResult> DeleteAsync(Guid CourseId)
    {
        var response = await catalogRefitService.DeleteCourseAsync(CourseId);
        if (!response.IsSuccessStatusCode)
        {
            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response.Error.Content!);
            logger.LogError("Error occurred while deleting course");
            return ServiceResult.Error("Fail to delete course. Please try again later");
        }

        return ServiceResult.Success();
    }
}