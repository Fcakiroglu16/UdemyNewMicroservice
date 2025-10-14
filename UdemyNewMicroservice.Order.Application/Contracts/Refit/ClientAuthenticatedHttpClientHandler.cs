#region

using Duende.IdentityModel.Client;
using Microsoft.Extensions.DependencyInjection;
using UdemyNewMicroservice.Shared.Options;

#endregion

namespace UdemyNewMicroservice.Order.Application.Contracts.Refit;

internal class ClientAuthenticatedHttpClientHandler(
    IServiceProvider serviceProvider,
    IHttpClientFactory httpClientFactory) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (request.Headers.Authorization is not null) return await base.SendAsync(request, cancellationToken);


        using var scope = serviceProvider.CreateScope();
        var identityOptions = scope.ServiceProvider.GetRequiredService<IdentityOption>();
        var clientSecretOption = scope.ServiceProvider.GetRequiredService<ClientSecretOption>();

        var discoveryRequest = new DiscoveryDocumentRequest
        {
            Address = identityOptions.Address,
            Policy = { RequireHttps = false }
        };

        var client = httpClientFactory.CreateClient();

        client.BaseAddress = new Uri(identityOptions.Address);
        var discoveryResponse = await client.GetDiscoveryDocumentAsync(cancellationToken: cancellationToken);


        if (discoveryResponse.IsError)
            throw new Exception($"Discovery document request failed: {discoveryResponse.Error}");


        var tokenResponse = await client.RequestClientCredentialsTokenAsync(
            new ClientCredentialsTokenRequest
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = clientSecretOption.Id,
                ClientSecret = clientSecretOption.Secret
            }, cancellationToken);

        if (tokenResponse.IsError) throw new Exception($"Token request failed: {tokenResponse.Error}");

        request.SetBearerToken(tokenResponse.AccessToken!);
        return await base.SendAsync(request, cancellationToken);
    }
}