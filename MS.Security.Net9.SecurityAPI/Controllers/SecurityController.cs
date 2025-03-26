namespace MS.Security.Net9.SecurityAPI.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MS.Security.Net9.SecurityAPI.DTOs;
using MS.Security.Net9.SecurityAPI.Services;

[ApiController]
[Route("api/[controller]")]
public class SecurityController(IOAuthService oAuthService) : ControllerBase
{
    private readonly IOAuthService oAuthService = oAuthService;

    [HttpPost("auth/token", Name = "GetToken")]
    public async Task<ActionResult<AuthResponse>> Auth([FromHeader] string Authorization)
    {
        if (string.IsNullOrEmpty(Authorization) || !Authorization.StartsWith("Bearer "))
        {
            return Unauthorized("Invalid or missing token");
        }

        string oAuthToken = Authorization.Substring("Bearer ".Length);
        AuthResponse tokenResponse = await oAuthService.Authorize(oAuthToken);

        return Ok(tokenResponse);
    }
}
