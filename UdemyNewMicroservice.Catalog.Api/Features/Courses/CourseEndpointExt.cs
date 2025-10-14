#region

using Asp.Versioning.Builder;
using UdemyNewMicroservice.Catalog.Api.Features.Courses.Create;
using UdemyNewMicroservice.Catalog.Api.Features.Courses.Delete;
using UdemyNewMicroservice.Catalog.Api.Features.Courses.GetAll;
using UdemyNewMicroservice.Catalog.Api.Features.Courses.GetAllByUserId;
using UdemyNewMicroservice.Catalog.Api.Features.Courses.GetById;
using UdemyNewMicroservice.Catalog.Api.Features.Courses.Update;

#endregion

namespace UdemyNewMicroservice.Catalog.Api.Features.Courses;

public static class CourseEndpointExt
{
    public static void AddCourseGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        app.MapGroup("api/v{version:apiVersion}/courses").WithTags("Courses").WithApiVersionSet(apiVersionSet)
            .CreateCourseGroupItemEndpoint()
            .GetAllCourseGroupItemEndpoint()
            .GetByIdCourseGroupItemEndpoint()
            .UpdateCourseGroupItemEndpoint()
            .DeleteCourseGroupItemEndpoint()
            .GetByUserIdCourseGroupItemEndpoint();
    }
}