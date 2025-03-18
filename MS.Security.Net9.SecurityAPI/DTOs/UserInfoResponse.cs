namespace MS.Security.Net9.SecurityAPI.DTOs;

using System.Text.Json.Serialization;

public record UserInfoResponse
{
    [JsonConstructor]
    public UserInfoResponse(string username, string email)
    {
        this.Username = username;
        this.Email = email;
    }

    public string Username { get; private set; }
    public string Email { get; private set; }
}