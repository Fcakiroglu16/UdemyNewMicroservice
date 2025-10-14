#region

using UdemyNewMicroservice.Catalog.Api.Features.Courses.Dtos;

#endregion

namespace UdemyNewMicroservice.Catalog.Api.Features.Courses.GetAll;

public record GetAllCoursesQuery : IRequestByServiceResult<List<CourseDto>>;

public class GetAllCoursesQueryHandler(AppDbContext context, IMapper mapper)
    : IRequestHandler<GetAllCoursesQuery, ServiceResult<List<CourseDto>>>
{
    public async Task<ServiceResult<List<CourseDto>>> Handle(GetAllCoursesQuery request,
        CancellationToken cancellationToken)
    {
        var courses = await context.Courses
            .ToListAsync(cancellationToken);

        var categories = await context.Categories.ToListAsync(cancellationToken);


        foreach (var course in courses) course.Category = categories.First(x => x.Id == course.CategoryId);

        var coursesAsDto = mapper.Map<List<CourseDto>>(courses);
        return ServiceResult<List<CourseDto>>.SuccessAsOk(coursesAsDto);
    }
}

public static class GetAllCoursesEndpoint
{
    public static RouteGroupBuilder GetAllCourseGroupItemEndpoint(this RouteGroupBuilder group)
    {
        group.MapGet("/",
                async (IMediator mediator) =>
                    (await mediator.Send(new GetAllCoursesQuery())).ToGenericResult())
            .MapToApiVersion(1, 0)
            .WithName("GetAllCourses");

        return group;
    }
}