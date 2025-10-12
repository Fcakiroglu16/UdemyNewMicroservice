namespace UdemyNewMicroservice.Web.Pages.Instructor.ViewModel;

public record CourseViewModel(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string ImageUrl,
    string CategoryName,
    int Duration,
    float Rating);