namespace MS.Security.Net9.SecurityAPI.Services;

using MS.Security.Net9.SecurityAPI.DTOs;

public class GoogleOpenIDConnectService(IHttpClientFactory httpClientFactory) : IOpenIDConnectService
{
    private readonly HttpClient httpClient = httpClientFactory.CreateClient(nameof(GoogleOpenIDConnectService));

    public async Task<UserInfoResponse> GetUserInfo(string oAuthToken)
    {
        try
        {
            var requestUrl = $"?access_token={oAuthToken}";
            var response = await httpClient.GetAsync(requestUrl);

            response.EnsureSuccessStatusCode();

            string responseString = response.Content.ReadAsStringAsync().Result;
            var userInfo = System.Text.Json.JsonSerializer.Deserialize<GoogleOpenIDConnectResponse>(responseString);
            var email = userInfo?.Email ?? throw new UnauthorizedAccessException("Invalid OAuth token");

            return new UserInfoResponse(email);
        }
        catch(Exception ex)
        {
            throw new UnauthorizedAccessException("Invalid OAuth token", ex);
        }
    }
}