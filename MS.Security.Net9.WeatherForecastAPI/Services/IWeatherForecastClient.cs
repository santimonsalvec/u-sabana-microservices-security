namespace MS.Security.Net9.WeatherForecastAPI.Services;

using MS.Security.Net9.WeatherForecastAPI.DTOs;

public interface IWeatherForecastClient
{
    Task<WeatherForecastResponse> GetForecast();
}