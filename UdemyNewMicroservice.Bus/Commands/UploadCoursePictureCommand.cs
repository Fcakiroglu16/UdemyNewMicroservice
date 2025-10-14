namespace UdemyNewMicroservice.Bus.Commands;

public record UploadCoursePictureCommand(Guid courseId, byte[] picture, string FileName);