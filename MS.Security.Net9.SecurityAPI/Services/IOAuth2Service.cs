namespace MS.Security.Net9.SecurityAPI.Services;

using MS.Security.Net9.SecurityAPI.DTOs;

public interface IOAuthService
{
    AuthResponse Authenticate(string username, string password);
}