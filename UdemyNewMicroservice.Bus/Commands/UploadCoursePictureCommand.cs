namespace UdemyNewMicroservice.Bus.Commands
{
    public record UploadCoursePictureCommand(Guid courseId, Byte[] picture);
}