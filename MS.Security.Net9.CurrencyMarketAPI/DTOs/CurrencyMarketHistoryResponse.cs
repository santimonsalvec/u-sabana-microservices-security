namespace MS.Security.Net9.CurrencyMarketAPI.DTOs;

using System.Text.Json.Serialization;

public class CurrencyMarketHistoryResponse
{
    [JsonPropertyName("prices")]
    public List<List<double>> Prices { get; set; }

    [JsonPropertyName("market_caps")]
    public List<List<double>> MarketCaps { get; set; }

    [JsonPropertyName("total_volumes")]
    public List<List<double>> TotalVolumes { get; set; }
}