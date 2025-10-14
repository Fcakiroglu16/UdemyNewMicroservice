#region

using UdemyNewMicroservice.Shared;

#endregion

namespace UdemyNewMicroservice.File.Api.Features.File.Upload;

public record UploadFileCommand(IFormFile File) : IRequestByServiceResult<UploadFileCommandResponse>;