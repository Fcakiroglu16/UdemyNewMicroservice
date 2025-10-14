#region

using MediatR;
using Microsoft.Extensions.FileProviders;
using UdemyNewMicroservice.Shared;

#endregion

namespace UdemyNewMicroservice.File.Api.Features.File.Delete;

public class DeleteFileCommandHandler(IFileProvider fileProvider) : IRequestHandler<DeleteFileCommand, ServiceResult>
{
    public Task<ServiceResult> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        var fileInfo = fileProvider.GetFileInfo(Path.Combine("files", request.FileName));

        if (!fileInfo.Exists) return Task.FromResult(ServiceResult.ErrorAsNotFound());

        System.IO.File.Delete(fileInfo.PhysicalPath!);

        return Task.FromResult(ServiceResult.SuccessAsNoContent());
    }
}