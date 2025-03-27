namespace MS.Security.Net9.WeatherForecastAPI;

using MS.Security.Net9.WeatherForecastAPI.Services;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddHttpClient<WeatherForecastClient>
        (
            client =>
            {
                client.BaseAddress = new Uri("https://api.open-meteo.com/v1/forecast?latitude=4.61&longitude=-74.08&hourly=temperature_2m");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            }
        );

        builder.Services.AddScoped<IWeatherForecastClient, WeatherForecastClient>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseCors("AllowAll");
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();

    }
}