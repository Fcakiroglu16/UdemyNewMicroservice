#region

using UdemyNewMicroservice.Catalog.Api.Features.Courses.Create;
using UdemyNewMicroservice.Catalog.Api.Features.Courses.Dtos;

#endregion

namespace UdemyNewMicroservice.Catalog.Api.Features.Courses;

public class CourseMapping : Profile
{
    public CourseMapping()
    {
        CreateMap<CreateCourseCommand, Course>();
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<Feature, FeatureDto>().ReverseMap();
    }
}