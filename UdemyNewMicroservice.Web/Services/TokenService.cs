#region

using Duende.IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UdemyNewMicroservice.Web.Options;

#endregion

namespace UdemyNewMicroservice.Web.Services;

public class TokenService(IHttpClientFactory httpClientFactory, IdentityOption identityOption)
{
    public List<Claim> ExtractClaims(string accessToken)
    {
        var handler = new JwtSecurityTokenHandler();

        var jwtWebToken = handler.ReadJwtToken(accessToken);


        return jwtWebToken.Claims.ToList();
    }

    public AuthenticationProperties CreateAuthenticationProperties(TokenResponse tokenResponse)
    {
        var authenticationTokens = new List<AuthenticationToken>
        {
            new()
            {
                Name = OpenIdConnectParameterNames.AccessToken,
                Value = tokenResponse.AccessToken!
            },
            new()
            {
                Name = OpenIdConnectParameterNames.RefreshToken,
                Value = tokenResponse.RefreshToken!
            },
            new()
            {
                Name = OpenIdConnectParameterNames.ExpiresIn,
                Value = DateTime.Now.AddSeconds(tokenResponse.ExpiresIn!).ToString("o")
            }
        };


        AuthenticationProperties authenticationProperties = new()
        {
            IsPersistent = true
        };

        authenticationProperties.StoreTokens(authenticationTokens);

        return authenticationProperties;
    }


    public async Task<TokenResponse> GetTokensByRefreshToken(string refreshToken)
    {
        var discoveryRequest = new DiscoveryDocumentRequest
        {
            Address = identityOption.Address,
            Policy = { RequireHttps = false }
        };
        var client = httpClientFactory.CreateClient("GetTokensByRefreshToken");
        client.BaseAddress = new Uri(identityOption.Address);
        var discoveryResponse = await client.GetDiscoveryDocumentAsync(discoveryRequest);

        if (discoveryResponse.IsError)
            throw new Exception($"Discovery document request failed: {discoveryResponse.Error}");


        var tokenResponse = await client.RequestRefreshTokenAsync(new RefreshTokenRequest
        {
            Address = discoveryResponse.TokenEndpoint,
            ClientId = identityOption.Web.ClientId,
            ClientSecret = identityOption.Web.ClientSecret,
            RefreshToken = refreshToken
        });


        return tokenResponse;
    }


    public async Task<TokenResponse> GetClientAccessToken()
    {
        var discoveryRequest = new DiscoveryDocumentRequest
        {
            Address = identityOption.Address,
            Policy = { RequireHttps = false }
        };

        var client = httpClientFactory.CreateClient("GetClientAccessToken");
        client.BaseAddress = new Uri(identityOption.Address);
        var discoveryResponse = await client.GetDiscoveryDocumentAsync(discoveryRequest);


        if (discoveryResponse.IsError)
            throw new Exception($"Discovery document request failed: {discoveryResponse.Error}");


        var tokenResponse = await client.RequestClientCredentialsTokenAsync(
            new ClientCredentialsTokenRequest
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = identityOption.Web.ClientId,
                ClientSecret = identityOption.Web.ClientSecret
            });

        if (tokenResponse.IsError) throw new Exception($"Token request failed: {tokenResponse.Error}");

        return tokenResponse;
    }
}