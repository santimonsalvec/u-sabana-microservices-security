namespace MS.Security.Net9.APIGateway;

using System.Text;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

public static class Program
{
    private const string defaultEnvironment = "Development";
    private const string environmentKey = "ASPNETCORE_ENVIRONMENT";

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string environmentName = Environment.GetEnvironmentVariable(environmentKey) ?? defaultEnvironment;
        IConfigurationRoot configuration = GetConfigurationBuilder(environmentName);
        builder.Configuration.AddConfiguration(configuration);

        // ✅ Autenticación JWT
        builder.Services
            .AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Token:Issuer"],
                    ValidAudience = configuration["Token:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Secret"]))
                };
            });

        builder.Services.AddAuthorization();

        builder.Services.AddOcelot(configuration);

        var app = builder.Build();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseHttpsRedirection(); // Opcional

        app.UseOcelot().Wait(); // <- Espera necesaria porque es async

        app.Run();
    }

    private static IConfigurationRoot GetConfigurationBuilder(string environmentName)
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
    }
}