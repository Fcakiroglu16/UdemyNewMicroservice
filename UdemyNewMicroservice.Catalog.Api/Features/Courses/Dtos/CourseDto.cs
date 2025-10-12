namespace UdemyNewMicroservice.Catalog.Api.Features.Courses.Dtos;

public record CourseDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string ImageUrl,
    CategoryDto Category,
    FeatureDto Feature);