using Duende.IdentityModel.Client;
using UdemyNewMicroservice.Web.Options;
using UdemyNewMicroservice.Web.Services;

namespace UdemyNewMicroservice.Web.Pages.Auth.SignIn
{
    public class SignInService(IdentityOption identityOption, HttpClient client, ILogger<SignInService> logger)
    {
        public async Task<ServiceResult> SignInAsync(SignInViewModel signInViewModel)
        {
            var tokenResponse = await GetAccessToken(signInViewModel);

            if (tokenResponse.IsError)
            {
                return ServiceResult.Error(tokenResponse.Error!, tokenResponse.ErrorDescription!);
            }


            return ServiceResult.Success();
        }


        private async Task<TokenResponse> GetAccessToken(SignInViewModel signInViewModel)
        {
            var discoveryRequest = new DiscoveryDocumentRequest()
            {
                Address = identityOption.Address,
                Policy = { RequireHttps = false }
            };

            client.BaseAddress = new Uri(identityOption.Address);
            var discoveryResponse = await client.GetDiscoveryDocumentAsync();

            if (discoveryResponse.IsError)
            {
                throw new Exception($"Discovery document request failed: {discoveryResponse.Error}");
            }


            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = identityOption.Web.ClientId,
                ClientSecret = identityOption.Web.ClientSecret,
                UserName = signInViewModel.Email,
                Password = signInViewModel.Password
            });

            return tokenResponse;
        }
    }
}