namespace MS.Security.Net9.WeatherForecastAPI.DTOs;

using System.Text.Json.Serialization;

public class Hourly
{
    [JsonPropertyName("time")]
    public List<string> Time { get; set; }

    [JsonPropertyName("temperature_2m")]
    public List<double> Temperature2m { get; set; }
}