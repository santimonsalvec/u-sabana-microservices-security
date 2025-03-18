namespace MS.Security.Net9.SecurityAPI.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MS.Security.Net9.SecurityAPI.DTOs;

public class OpenIDConnectService : IOpenIDConnectService
{
    public UserInfoResponse GetUserInfo(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        var claims = jwtToken.Claims;

        string? username = claims.Single(c => c.Type == ClaimTypes.Name).Value;
        string? email = claims.Single(c => c.Type == ClaimTypes.Email).Value;

        return new UserInfoResponse(username, email);
    }
}