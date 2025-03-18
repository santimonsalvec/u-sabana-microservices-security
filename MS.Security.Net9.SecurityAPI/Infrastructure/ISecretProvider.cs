namespace MS.Security.Net9.SecurityAPI.Infrastructure;

public interface ISecretProvider
{
    string? GetSecret(string key);
}