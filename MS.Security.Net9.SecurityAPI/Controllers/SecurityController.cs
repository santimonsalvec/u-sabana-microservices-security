namespace MS.Security.Net9.SecurityAPI.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MS.Security.Net9.SecurityAPI.DTOs;
using MS.Security.Net9.SecurityAPI.Services;

[ApiController]
[Route("api/[controller]")]
public class SecurityController(IOAuthService oAuthService, IOpenIDConnectService openIDConnectService) : ControllerBase
{
    private readonly IOAuthService oAuthService = oAuthService;
    private readonly IOpenIDConnectService openIDConnectService = openIDConnectService;

    [HttpPost("auth", Name = "Authentication")]
    public ActionResult<AuthResponse> Auth(AuthRequest authRequest)
    {
        AuthResponse tokenResponse = oAuthService.Authenticate(authRequest.Username, authRequest.Password);
        return Ok(tokenResponse);
    }

    [Authorize]
    [HttpGet("user-info", Name = "GetUserInfo")]
    public IActionResult GetUserInfo([FromHeader] string Authorization)
    {
        if (string.IsNullOrEmpty(Authorization) || !Authorization.StartsWith("Bearer "))
        {
            return Unauthorized("Invalid or missing token");
        }

        string token = Authorization.Substring("Bearer ".Length);
        UserInfoResponse? userInfo = openIDConnectService.GetUserInfo(token);

        if (userInfo is null)
        {
            return Unauthorized("Invalid token");
        }

        return Ok(userInfo);
    }
}
