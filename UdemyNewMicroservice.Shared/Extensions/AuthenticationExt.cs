using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using UdemyNewMicroservice.Shared.Options;

namespace UdemyNewMicroservice.Shared.Extensions
{
    public static class AuthenticationExt
    {

        public static IServiceCollection AddAuthenticationAndAuthorizationExt(this IServiceCollection services, IConfiguration configuration)
        {

            var identityOptions = configuration.GetSection(nameof(IdentityOption)).Get<IdentityOption>();


            services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {

                options.Authority = identityOptions.Address;
                options.Audience = identityOptions.Audience;
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters()
                {

                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateIssuer = true

                };


            });
            services.AddAuthorization();

            // Sign
            // Aud  => payment.api
            // Issuer => http://localhost:8080/realms/udemyTenant
            // TokenLifetime

            return services;
        }
    }
}
