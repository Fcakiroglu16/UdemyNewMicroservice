#region

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

#endregion

namespace UdemyNewMicroservice.Web.Pages.Instructor.ViewModel;

public record CreateCourseViewModel
{
    public static CreateCourseViewModel Empty => new();


    [Display(Name = "Course Category")] public SelectList CategoryDropdownList { get; set; } = null!;


    [Display(Name = "Course Picture")] public IFormFile? PictureFormFile { get; init; }


    [Display(Name = "Course Name")] public string Name { get; init; } = null!;


    [Display(Name = "Course Description")] public string Description { get; init; } = null!;


    [Display(Name = "Course Price")] public decimal Price { get; init; }

    public Guid? CategoryId { get; init; }


    public void SetCategoryDropdownList(List<CategoryViewModel> categories)
    {
        CategoryDropdownList = new SelectList(categories, "Id", "Name");
    }
}