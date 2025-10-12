namespace UdemyNewMicroservice.Catalog.Api.Features.Courses.Create;

public record CreateCourseCommand : IRequestByServiceResult<Guid>
{
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public decimal Price { get; init; }
    public Guid CategoryId { get; init; }

    public IFormFile? Picture { get; set; }
}