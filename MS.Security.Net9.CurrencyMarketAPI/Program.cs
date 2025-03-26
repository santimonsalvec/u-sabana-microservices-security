namespace MS.Security.Net9.CurrencyMarketAPI;

using MS.Security.Net9.CurrencyMarketAPI.Services;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddHttpClient<CurrencyMarketClient>
        (
            client =>
            {
                client.BaseAddress = new Uri("https://api.coingecko.com/api/v3/coins/bitcoin/market_chart?vs_currency=usd&days=90");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            }
        );

        builder.Services.AddScoped<ICurrencyMarketClient, CurrencyMarketClient>();
        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }
}