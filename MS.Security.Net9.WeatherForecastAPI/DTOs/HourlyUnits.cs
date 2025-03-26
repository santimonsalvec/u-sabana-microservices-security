namespace MS.Security.Net9.WeatherForecastAPI.DTOs;

using System.Text.Json.Serialization;

public class HourlyUnits
{
    [JsonPropertyName("time")]
    public string Time { get; set; }

    [JsonPropertyName("temperature_2m")]
    public string Temperature2m { get; set; }
}