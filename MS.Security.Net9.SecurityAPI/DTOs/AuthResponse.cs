namespace MS.Security.Net9.SecurityAPI.DTOs;

using System.Text.Json.Serialization;

public record AuthResponse
{
    [JsonConstructor]
    public AuthResponse(string accessToken, string tokenType, int expiresIn, string idToken)
    {
        AccessToken = accessToken;
        TokenType = tokenType;
        ExpiresIn = expiresIn;
        IdToken = idToken;
    }

    [JsonPropertyName("accessToken")]
    public string AccessToken { get; private set; }

    [JsonPropertyName("tokenType")]
    public string TokenType { get; private set; }

    [JsonPropertyName("expiresIn")]
    public int ExpiresIn { get; private set; }

    [JsonPropertyName("idToken")]
    public string IdToken { get; private set; }
}