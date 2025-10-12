namespace UdemyNewMicroservice.Web.Pages.Instructor.Dto;

public class FeatureDto
{
    public int Duration { get; set; }
    public float Rating { get; set; }

    public string EducatorFullName { get; set; } = default!;
}