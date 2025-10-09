#region

using Microsoft.AspNetCore.Mvc.RazorPages;
using UdemyNewMicroservice.Web.Pages.Instructor.ViewModel;

#endregion

namespace UdemyNewMicroservice.Web.Pages.Instructor;

public class CreateCourseModel : PageModel
{
    public CreateCourseViewModel ViewModel { get; set; } = CreateCourseViewModel.Empty;

    public void OnGet()
    {
    }
}