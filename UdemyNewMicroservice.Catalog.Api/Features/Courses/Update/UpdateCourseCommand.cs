namespace UdemyNewMicroservice.Catalog.Api.Features.Courses.Update;

public record UpdateCourseCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string? ImageUrl,
    Guid CategoryId) : IRequestByServiceResult;