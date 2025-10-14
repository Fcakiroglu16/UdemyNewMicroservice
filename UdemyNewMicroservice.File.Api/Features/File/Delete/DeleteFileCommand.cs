#region

using UdemyNewMicroservice.Shared;

#endregion

namespace UdemyNewMicroservice.File.Api.Features.File.Delete;

public record DeleteFileCommand(string FileName) : IRequestByServiceResult;