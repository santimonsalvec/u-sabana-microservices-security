namespace MS.Security.Net9.SecurityAPI.AppConfig;

public class TokenSetting
{
    public string BaseUrl { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string Secret { get; set; }
}