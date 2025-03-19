namespace MS.Security.Net9.SecurityAPI.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MS.Security.Net9.SecurityAPI.AppConfig;
using MS.Security.Net9.SecurityAPI.DTOs;

public class OAuthService(Settings settings, IOpenIDConnectService openIDConnectService) : IOAuthService
{   
    private readonly IOpenIDConnectService openIDConnectService = openIDConnectService;
    private const string tokenType = "Bearer";
    private const int tokenTTLInSeconds = 3600;
    private readonly Settings settings = settings;

    public async Task<AuthResponse> Authorize(string oAuthToken)
    {
        UserInfoResponse userInfo = await this.openIDConnectService.GetUserInfo(oAuthToken);
        string token = await GenerateJwtToken(userInfo.Email);
        
        return new AuthResponse(token, tokenType, tokenTTLInSeconds);
    }

    private async Task<string> GenerateJwtToken(string email)
    {
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.settings.Token.Secret));
        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: this.settings.Token.Issuer,
            audience: this.settings.Token.Audience,
            claims: 
            [
                new Claim(ClaimTypes.Email, email)
            ],
            expires: DateTime.UtcNow.AddSeconds(tokenTTLInSeconds),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
