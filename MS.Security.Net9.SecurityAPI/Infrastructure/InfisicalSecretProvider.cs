using Infisical.Sdk;
using MS.Security.Net9.SecurityAPI.AppConfig;

namespace MS.Security.Net9.SecurityAPI.Infrastructure;

public class InfisicalSecretProvider : ISecretProvider
{
    private readonly InfisicalClient infisicalClient;
    private readonly Settings appSettings;

    public InfisicalSecretProvider(Settings appSettings)
    {
        this.appSettings = appSettings;

        ClientSettings settings = new()
        {
            Auth = new AuthenticationOptions
            {
                AccessToken = this.appSettings.Infisical.ClientSecret
            }
        };

        this.infisicalClient = new(settings);
        Console.WriteLine("Secret client --> ", this.GetSecret("app-secret"));
    }
    
    public string? GetSecret(string key)
    {
        GetSecretResponseSecret result = this.infisicalClient.GetSecret(this.BuildSecretOptions(key));
        return result.SecretValue ?? null;
    }

    private GetSecretOptions BuildSecretOptions(string key)
    {
        return  new GetSecretOptions()
        {
            SecretName = key,
            ProjectId = this.appSettings.Infisical.ProjectId,
            Environment = this.appSettings.Infisical.Environment,
        };
    }
}