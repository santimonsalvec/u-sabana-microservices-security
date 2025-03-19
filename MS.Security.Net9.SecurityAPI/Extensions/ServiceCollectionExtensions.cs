namespace MS.Security.Net9.SecurityAPI.Extensions;

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MS.Security.Net9.SecurityAPI.AppConfig;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfigurationRoot configuration)
    {
        services.Configure<Settings>(configuration);
        services.AddSingleton<Settings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<Settings>>().Value);

        return services;
    }

    public static IServiceCollection UseAuthentication(this IServiceCollection services, Settings appSettings)
    {
        services.AddAuthentication
        (
            config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
        )
        .AddJwtBearer
        (
            options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = appSettings.Token.Issuer,
                    ValidAudience = appSettings.Token.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Token.Secret))
                };
            }
        );
                
        return services;
    }
}