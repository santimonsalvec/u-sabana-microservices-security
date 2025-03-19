namespace MS.Security.Net9.SecurityAPI;

using MS.Security.Net9.SecurityAPI.Services;
using MS.Security.Net9.SecurityAPI.AppConfig;
using MS.Security.Net9.SecurityAPI.Extensions;

public static class Program
{
    private const string defaultEnvironment = "Development";
    private const string environmentKey = "ASPNETCORE_ENVIRONMENT";

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string environmentName = Environment.GetEnvironmentVariable(environmentKey) ?? defaultEnvironment;
        IConfigurationRoot configuration = GetConfigurationBuilder(environmentName);
        builder.Services.AddAppSettings(configuration);

        Settings appSettings = configuration.Get<Settings>()
            ?? throw new InvalidOperationException("AppSettings section not found");

        // Add services to the container.
        builder.Services.UseAuthentication(appSettings);
        builder.Services.AddHttpClient<GoogleOpenIDConnectService>
        (
            client =>
            {
                client.BaseAddress = new Uri(appSettings.Token.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            }
        );
        builder.Services.AddSingleton(appSettings);
        builder.Services.AddTransient<IOAuthService, OAuthService>();
        builder.Services.AddTransient<IOpenIDConnectService, GoogleOpenIDConnectService>();
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication(); // Add this line
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }

    private static IConfigurationRoot GetConfigurationBuilder(string environmentName)
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
    }
}
