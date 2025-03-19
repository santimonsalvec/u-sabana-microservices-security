namespace MS.Security.Net9.SecurityAPI.DTOs;

using System.Text.Json.Serialization;

public record AuthRequest
{
    [JsonPropertyName("oAuthToken")]
    public string OAuthToken { get; set; }
}