namespace MS.Security.Net9.SecurityAPI.DTOs;

using System.Text.Json.Serialization;

public record AuthRequest
{
    [JsonPropertyName("Username")]
    public string Username { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }
}