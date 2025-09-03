using UdemyNewMicroservice.Bus.Commands;

namespace UdemyNewMicroservice.Catalog.Api.Features.Courses.Create
{
    public class CreateCourseCommandHandler(AppDbContext context, IMapper mapper, IPublishEndpoint publishEndpoint)
        : IRequestHandler<CreateCourseCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var hasCategory = await context.Categories.AnyAsync(x => x.Id == request.CategoryId, cancellationToken);


            if (!hasCategory)
            {
                return ServiceResult<Guid>.Error("Category not found.",
                    $"The Category with id({request.CategoryId}) was not found", HttpStatusCode.NotFound);
            }


            var hasCourse = await context.Courses.AnyAsync(x => x.Name == request.Name, cancellationToken);

            if (hasCourse)
            {
                return ServiceResult<Guid>.Error("Course already exists.",
                    $"The Course with name({request.Name}) already exists", HttpStatusCode.BadRequest);
            }


            var newCourse = mapper.Map<Course>(request);
            newCourse.Created = DateTime.Now;
            newCourse.Id = NewId.NextSequentialGuid(); // index performance
            newCourse.Feature = new Feature()
            {
                Duration = 10, // calculate by course video
                EducatorFullName = "Ahmet Yılmaz", // get by token payload
                Rating = 0
            };

            context.Courses.Add(newCourse);
            await context.SaveChangesAsync(cancellationToken);

            if (request.Picture is not null)
            {
                using var memoryStream = new MemoryStream();
                await request.Picture.CopyToAsync(memoryStream, cancellationToken);

                var PictureAsByteArray = memoryStream.ToArray();

                var uploadCoursePictureCommand =
                    new UploadCoursePictureCommand(newCourse.Id, PictureAsByteArray);

                await publishEndpoint.Publish(uploadCoursePictureCommand, cancellationToken);
            }


            return ServiceResult<Guid>.SuccessAsCreated(newCourse.Id, $"/api/courses/{newCourse.Id}");
        }
    }
}