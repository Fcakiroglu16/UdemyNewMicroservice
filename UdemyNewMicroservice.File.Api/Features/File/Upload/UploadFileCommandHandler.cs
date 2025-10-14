#region

using System.Net;
using MediatR;
using Microsoft.Extensions.FileProviders;
using UdemyNewMicroservice.Shared;

#endregion

namespace UdemyNewMicroservice.File.Api.Features.File.Upload;

public class UploadFileCommandHandler(IFileProvider fileProvider)
    : IRequestHandler<UploadFileCommand, ServiceResult<UploadFileCommandResponse>>
{
    public async Task<ServiceResult<UploadFileCommandResponse>> Handle(UploadFileCommand request,
        CancellationToken cancellationToken)
    {
        if (request.File.Length == 0)
            return ServiceResult<UploadFileCommandResponse>.Error("Invalid file", "The provided file is empty or null",
                HttpStatusCode.BadRequest);

        var newFileName = $"{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}"; // .jpg

        var uploadPath = Path.Combine(fileProvider.GetFileInfo("files").PhysicalPath!, newFileName);


        await using var stream = new FileStream(uploadPath, FileMode.Create);


        await request.File.CopyToAsync(stream, cancellationToken);


        var response = new UploadFileCommandResponse(newFileName, $"files/{newFileName}", request.File.FileName);

        return ServiceResult<UploadFileCommandResponse>.SuccessAsCreated(response, response.FilePath);
    }
}