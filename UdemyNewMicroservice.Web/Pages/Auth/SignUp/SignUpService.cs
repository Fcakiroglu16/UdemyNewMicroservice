#region

using Duende.IdentityModel.Client;
using System.Net;
using UdemyNewMicroservice.Web.Options;
using UdemyNewMicroservice.Web.Services;

#endregion

namespace UdemyNewMicroservice.Web.Pages.Auth.SignUp;

public record KeycloakErrorResponse(string ErrorMessage);

public class SignUpService(IdentityOption identityOption, HttpClient client, ILogger<SignUpService> logger)
{
    public async Task<ServiceResult> CreateAccount(SignUpViewModel model)
    {
        var token = await GetClientCredentialTokenAsAdmin();

        var address = $"{identityOption.BaseAddress}/admin/realms/udemyTenant/users";

        client.SetBearerToken(token);

        var userCreateRequest = CreateUserCreateRequest(model);

        var response = await client.PostAsJsonAsync(address, userCreateRequest);
        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode != HttpStatusCode.InternalServerError)
            {
                var keycloakErrorResponse = await response.Content.ReadFromJsonAsync<KeycloakErrorResponse>();

                return ServiceResult.Error(keycloakErrorResponse!.ErrorMessage);
            }

            var error = await response.Content.ReadAsStringAsync();
            logger.LogError(error);
            return ServiceResult.Error("System Error occurred. Please try again later.");
        }

        return ServiceResult.Success();
    }

    private static UserCreateRequest CreateUserCreateRequest(SignUpViewModel model)
    {
        return new UserCreateRequest(
            model.UserName,
            true,
            model.FirstName,
            model.LastName,
            model.Email,
            [new Credential("password", model.Password, false)]);
    }

    private async Task<string> GetClientCredentialTokenAsAdmin()
    {
        var discoveryRequest = new DiscoveryDocumentRequest
        {
            Address = identityOption.Address,
            Policy = { RequireHttps = false }
        };

        client.BaseAddress = new Uri(identityOption.Address);
        var discoveryResponse = await client.GetDiscoveryDocumentAsync(discoveryRequest);

        if (discoveryResponse.IsError)
            throw new Exception($"Discovery document request failed: {discoveryResponse.Error}");


        var tokenResponse = await client.RequestClientCredentialsTokenAsync(
            new ClientCredentialsTokenRequest
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = identityOption.Admin.ClientId,
                ClientSecret = identityOption.Admin.ClientSecret
            });

        if (tokenResponse.IsError) throw new Exception($"Token request failed: {tokenResponse.Error}");

        return tokenResponse.AccessToken!;
    }
}