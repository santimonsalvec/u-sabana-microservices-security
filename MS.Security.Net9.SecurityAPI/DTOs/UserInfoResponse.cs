namespace MS.Security.Net9.SecurityAPI.DTOs;

using System.Text.Json.Serialization;

public record UserInfoResponse
{
    [JsonConstructor]
    public UserInfoResponse(string email)
    {
        this.Email = email;
    }

    public string Email { get; private set; }
}