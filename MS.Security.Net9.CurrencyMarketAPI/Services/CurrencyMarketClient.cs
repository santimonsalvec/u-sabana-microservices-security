namespace MS.Security.Net9.CurrencyMarketAPI.Services;

using System.Text.Json;
using MS.Security.Net9.CurrencyMarketAPI.DTOs;

public class CurrencyMarketClient(IHttpClientFactory httpClientFactory) : ICurrencyMarketClient
{
    private readonly HttpClient httpClient = httpClientFactory.CreateClient(nameof(CurrencyMarketClient));

    public async Task<CurrencyMarketHistoryResponse> GetHistory()
    {
        try
        {
            var response = await httpClient.GetAsync(httpClient.BaseAddress);
            response.EnsureSuccessStatusCode();
            string responseString = response.Content.ReadAsStringAsync().Result;

            return JsonSerializer.Deserialize<CurrencyMarketHistoryResponse>(responseString);
        }
        catch(Exception ex)
        {
            throw new UnauthorizedAccessException("Error on currency market service", ex);
        }
    }
}