#region

using UdemyNewMicroservice.Web.Pages.Instructor.Dto;

#endregion

namespace UdemyNewMicroservice.Web.Dto;

public record CourseDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string ImageUrl,
    DateTime Created,
    CategoryDto Category,
    FeatureDto Feature);