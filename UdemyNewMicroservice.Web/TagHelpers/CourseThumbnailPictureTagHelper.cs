#region

using Microsoft.AspNetCore.Razor.TagHelpers;
using UdemyNewMicroservice.Web.Options;

#endregion

namespace UdemyNewMicroservice.Web.TagHelpers;

public class CourseThumbnailPictureTagHelper(MicroserviceOption microserviceOption) : TagHelper
{
    public string? Src { get; set; }


    public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "img";

        var blankCourseThumbnailImagePath = "/images/blank_course_thumbnail.jpg";

        if (string.IsNullOrEmpty(Src))
        {
            output.Attributes.SetAttribute("src", blankCourseThumbnailImagePath);
        }
        else
        {
            var courseThumbnailImagePath = $"{microserviceOption.File.BaseAddress}/{Src}";


            output.Attributes.SetAttribute("src", courseThumbnailImagePath);
        }


        return base.ProcessAsync(context, output);
    }
}