namespace MS.Security.Net9.CurrencyMarketAPI.Services;

using MS.Security.Net9.CurrencyMarketAPI.DTOs;

public interface ICurrencyMarketClient
{
    Task<CurrencyMarketHistoryResponse> GetHistory();
}