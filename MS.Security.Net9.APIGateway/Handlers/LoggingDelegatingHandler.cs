namespace MS.Security.Net9.APIGateway.Handlers;

public class LoggingDelegatingHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Console.WriteLine($"🛰️ Ocelot handler: {request.Method} {request.RequestUri}");
        return await base.SendAsync(request, cancellationToken);
    }
}