namespace UdemyNewMicroservice.Web.ViewModel;

public record CourseViewModel(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string ImageUrl,
    string Created,
    string EducatorFullName,
    string CategoryName,
    int Duration,
    float Rating)
{
    public string TruncateDescription(int maxLength)
    {
        if (Description.Length <= maxLength) return Description;
        return Description.Substring(0, maxLength) + "...";
    }
}