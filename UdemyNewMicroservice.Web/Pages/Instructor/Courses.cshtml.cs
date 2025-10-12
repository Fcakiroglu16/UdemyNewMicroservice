#region

using Microsoft.AspNetCore.Mvc.RazorPages;
using UdemyNewMicroservice.Web.Services;

#endregion

namespace UdemyNewMicroservice.Web.Pages.Instructor;

public class CoursesModel(CatalogService catalogService) : PageModel
{
    public void OnGet()
    {
    }
}