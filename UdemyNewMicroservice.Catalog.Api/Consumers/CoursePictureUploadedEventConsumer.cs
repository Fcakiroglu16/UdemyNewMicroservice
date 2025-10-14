#region

using UdemyNewMicroservice.Bus.Events;

#endregion

namespace UdemyNewMicroservice.Catalog.Api.Consumers;

public class CoursePictureUploadedEventConsumer(IServiceProvider serviceProvider)
    : IConsumer<CoursePictureUploadedEvent>
{
    public async Task Consume(ConsumeContext<CoursePictureUploadedEvent> context)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var course = dbContext.Courses.Find(context.Message.CourseId);
        if (course == null) throw new NotImplementedException();
        course.ImageUrl = context.Message.ImageUrl;
        await dbContext.SaveChangesAsync();
    }
}