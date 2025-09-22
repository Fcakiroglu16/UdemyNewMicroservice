using Duende.IdentityModel.Client;
using UdemyNewMicroservice.Web.Options;

namespace UdemyNewMicroservice.Web.Pages.Auth.SignUp
{
    public class SignUpService(IdentityOption identityOption, HttpClient client)
    {
        public async Task<string> GetClientCredentialTokenAsAdmin()
        {
            var discoveryRequest = new DiscoveryDocumentRequest()
            {
                Address = identityOption.Admin.Address,
                Policy = { RequireHttps = false }
            };

            client.BaseAddress = new Uri(identityOption.Admin.Address);
            var discoveryResponse = await client.GetDiscoveryDocumentAsync();

            if (discoveryResponse.IsError)
            {
                throw new Exception($"Discovery document request failed: {discoveryResponse.Error}");
            }


            var tokenResponse = await client.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = discoveryResponse.TokenEndpoint,
                    ClientId = identityOption.Admin.ClientId,
                    ClientSecret = identityOption.Admin.ClientSecret,
                });

            if (tokenResponse.IsError)
            {
                throw new Exception($"Token request failed: {tokenResponse.Error}");
            }

            return tokenResponse.AccessToken!;
        }
    }
}