#region

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

#endregion

namespace UdemyNewMicroservice.Shared.ExceptionHandlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Title = "An error occurred while processing your request",
            Type = exception.GetType().Name,
            Status = (int)HttpStatusCode.InternalServerError
        }, cancellationToken);
        return true;
    }
}