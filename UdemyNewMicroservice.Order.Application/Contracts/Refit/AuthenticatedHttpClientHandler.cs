#region

using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

#endregion

namespace UdemyNewMicroservice.Order.Application.Contracts.Refit;

internal class AuthenticatedHttpClientHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (httpContextAccessor.HttpContext is null) return await base.SendAsync(request, cancellationToken);

        if (!httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated)
            return await base.SendAsync(request, cancellationToken);


        string? token = null;

        if (httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
            // Bearer abc
            token = authHeader.ToString().Split(" ")[1];


        if (!string.IsNullOrEmpty(token))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);


        return await base.SendAsync(request, cancellationToken);
    }
}