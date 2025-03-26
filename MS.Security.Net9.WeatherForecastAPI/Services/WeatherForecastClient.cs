namespace MS.Security.Net9.WeatherForecastAPI.Services;

using System.Text.Json;
using MS.Security.Net9.WeatherForecastAPI.DTOs;

public class WeatherForecastClient(IHttpClientFactory httpClientFactory) : IWeatherForecastClient
{
    private readonly HttpClient httpClient = httpClientFactory.CreateClient(nameof(WeatherForecastClient));

    public async Task<WeatherForecastResponse> GetForecast()
    {
        try
        {
            var response = await httpClient.GetAsync(httpClient.BaseAddress);
            response.EnsureSuccessStatusCode();
            string responseString = response.Content.ReadAsStringAsync().Result;

            return JsonSerializer.Deserialize<WeatherForecastResponse>(responseString);
        }
        catch(Exception ex)
        {
            throw new UnauthorizedAccessException("Error on weather forecaster", ex);
        }
    }
}