namespace MS.Security.Net9.WeatherForecastAPI.Controllers;

using Microsoft.AspNetCore.Mvc;
using MS.Security.Net9.WeatherForecastAPI.DTOs;
using MS.Security.Net9.WeatherForecastAPI.Services;

[ApiController]
[Route("api/weather-forecast")]
public class WeatherForecastController(IWeatherForecastClient weatherForecastClient) : ControllerBase
{
    private readonly IWeatherForecastClient weatherForecastClient = weatherForecastClient;

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<ActionResult<WeatherForecastResponse>> GetForecast()
    {
        try
        {
            return this.Ok(await this.weatherForecastClient.GetForecast());
        }
        catch(Exception ex)
        {
            return this.Problem(ex.Message);
        }
    }
}
