#region

using Microsoft.AspNetCore.Mvc;
using UdemyNewMicroservice.Web.PageModels;
using UdemyNewMicroservice.Web.ViewModel;

#endregion

namespace UdemyNewMicroservice.Web.Pages;

public class IndexModel(CatalogService catalogService, ILogger<IndexModel> logger) : BasePageModel
{
    public List<CourseViewModel>? Courses { get; set; } = [];


    public async Task<IActionResult> OnGet()
    {
        var coursesAsResult = await catalogService.GetAllCoursesAsync();

        if (coursesAsResult.IsFail) return ErrorPage(coursesAsResult);

        Courses = coursesAsResult.Data!;

        return Page();
    }
}