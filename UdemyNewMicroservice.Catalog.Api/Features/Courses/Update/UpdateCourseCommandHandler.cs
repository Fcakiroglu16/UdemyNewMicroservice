namespace UdemyNewMicroservice.Catalog.Api.Features.Courses.Update;

public class UpdateCourseCommandHandler(AppDbContext context, IMapper mapper)
    : IRequestHandler<UpdateCourseCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var hasCourse = await context.Courses.FindAsync([request.Id], cancellationToken);
        if (hasCourse == null) return ServiceResult.ErrorAsNotFound();

        hasCourse.Name = request.Name;
        hasCourse.Description = request.Description;
        hasCourse.Price = request.Price;
        hasCourse.ImageUrl = request.ImageUrl;
        hasCourse.CategoryId = request.CategoryId;


        context.Courses.Update(hasCourse);


        await context.SaveChangesAsync(cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}