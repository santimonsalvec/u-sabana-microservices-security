namespace MS.Security.Net9.SecurityAPI.DTOs;

using System.Text.Json.Serialization;

public class GoogleOpenIDConnectResponse
{
    [JsonPropertyName("email")]
    public string Email { get; set; }
}