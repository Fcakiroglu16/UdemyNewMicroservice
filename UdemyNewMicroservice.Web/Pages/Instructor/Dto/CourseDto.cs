namespace UdemyNewMicroservice.Web.Pages.Instructor.Dto;

public record CourseDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string ImageUrl,
    CategoryDto Category,
    FeatureDto Feature);