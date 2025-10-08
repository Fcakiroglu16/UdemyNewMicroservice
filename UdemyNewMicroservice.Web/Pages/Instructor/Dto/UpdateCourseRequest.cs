namespace UdemyNewMicroservice.Web.Pages.Instructor.Dto;

public record UpdateCourseRequest(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string? ImageUrl,
    Guid CategoryId);