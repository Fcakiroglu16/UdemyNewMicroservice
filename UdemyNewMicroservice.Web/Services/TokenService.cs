using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace UdemyNewMicroservice.Web.Services
{
    public class TokenService
    {
        public List<Claim> ExtractClaims(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwtWebToken = handler.ReadJwtToken(accessToken);


            return jwtWebToken.Claims.ToList();
        }

        public AuthenticationProperties
    }
}