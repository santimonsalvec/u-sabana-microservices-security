namespace MS.Security.Net9.SecurityAPI.Services;

using MS.Security.Net9.SecurityAPI.DTOs;

public interface IOpenIDConnectService
{
    UserInfoResponse GetUserInfo(string token);
}