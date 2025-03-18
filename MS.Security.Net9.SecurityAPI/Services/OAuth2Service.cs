namespace MS.Security.Net9.SecurityAPI.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MS.Security.Net9.SecurityAPI.AppConfig;
using MS.Security.Net9.SecurityAPI.DTOs;

public class OAuthService(Settings settings) : IOAuthService
{   
    private const string tokenType = "Bearer";
    private const int tokenTTLInSeconds = 3600;
    private const string subTokenKey = "sub";
    private const string emailTokenKey = "email";
    private readonly Settings settings = settings;

    public AuthResponse Authenticate(string username, string password)
    {
        if (string.Equals(username, "admin") && string.Equals(password, "admin"))
        {
            string token = GenerateJwtToken(username);
            string tokenId = GenerateIdToken(username);
            
            return new AuthResponse(token, tokenType, tokenTTLInSeconds, tokenId);
        }

        throw new UnauthorizedAccessException("Invalid credentials");
    }

    private string GenerateJwtToken(string username)
    {
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.settings.Token.Secret));
        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        string userEmail = $"{username}@{this.settings.Token.Audience}";

        var token = new JwtSecurityToken(
            issuer: this.settings.Token.Issuer,
            audience: this.settings.Token.Audience,
            claims: 
            [
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Email, userEmail)
            ],
            expires: DateTime.UtcNow.AddSeconds(tokenTTLInSeconds),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateIdToken(string username)
    {
        string userEmail = $"{username}@{this.settings.Token.Audience}";

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, username),
            new(subTokenKey, username),
            new(emailTokenKey, userEmail)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.settings.Token.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: this.settings.Token.Audience,
            audience: this.settings.Token.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(60),
            signingCredentials: creds
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
