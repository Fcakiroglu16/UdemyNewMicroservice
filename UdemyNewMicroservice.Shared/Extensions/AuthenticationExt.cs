#region

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using UdemyNewMicroservice.Shared.Options;

#endregion

namespace UdemyNewMicroservice.Shared.Extensions;

public static class AuthenticationExt
{
    public static IServiceCollection AddAuthenticationAndAuthorizationExt(this IServiceCollection services,
        IConfiguration configuration)
    {
        var identityOptions = configuration.GetSection(nameof(IdentityOption)).Get<IdentityOption>();


        services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.Authority = identityOptions.Address;
            options.Audience = identityOptions.Audience;
            options.RequireHttpsMetadata = false;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidateIssuer = true,
                RoleClaimType = ClaimTypes.Role,
                NameClaimType = ClaimTypes.NameIdentifier
            };


            // AutomaticRefreshInterval: otomatik aralıkla metadata/JWKS yenileme (ör. 24saat)
            options.AutomaticRefreshInterval = TimeSpan.FromHours(24);

            // RefreshInterval: RequestRefresh() çağrıldıktan sonra beklenen min süre (ör. 30s)
            options.RefreshInterval = TimeSpan.FromSeconds(30);
        }).AddJwtBearer("ClientCredentialSchema", options =>
        {
            options.Authority = identityOptions.Address;
            options.Audience = identityOptions.Audience;
            options.RequireHttpsMetadata = false;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidateIssuer = true
            };
        });
        services.AddAuthorization(options =>
        {
            options.AddPolicy("InstructorPolicy", policy =>
            {
                policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(ClaimTypes.Email);
                policy.RequireRole(ClaimTypes.Role, "instructor");
            });


            options.AddPolicy("Password", policy =>
            {
                policy.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                policy.RequireAuthenticatedUser();
                policy.RequireClaim(ClaimTypes.Email);
            });

            options.AddPolicy("ClientCredential", policy =>
            {
                policy.AuthenticationSchemes.Add("ClientCredentialSchema");
                policy.RequireAuthenticatedUser();
            });
        });

        // Sign
        // Aud  => payment.api
        // Issuer => http://localhost:8080/realms/udemyTenant
        // TokenLifetime

        return services;
    }
}