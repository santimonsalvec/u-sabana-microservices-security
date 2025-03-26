namespace MS.Security.Net9.CurrencyMarketAPI.Controllers;

using Microsoft.AspNetCore.Mvc;
using MS.Security.Net9.CurrencyMarketAPI.DTOs;
using MS.Security.Net9.CurrencyMarketAPI.Services;

[ApiController]
[Route("api/currency-market")]
public class WeatherForecastController(ICurrencyMarketClient currencyMarketClient) : ControllerBase
{
    private readonly ICurrencyMarketClient currencyMarketClient = currencyMarketClient;

    [HttpGet(Name = "GetCurrencyMarketHistory")]
    public async Task<ActionResult<CurrencyMarketHistoryResponse>> GetForecast()
    {
        try
        {
            return this.Ok(await this.currencyMarketClient.GetHistory());
        }
        catch(Exception ex)
        {
            return this.Problem(ex.Message);
        }
    }
}
