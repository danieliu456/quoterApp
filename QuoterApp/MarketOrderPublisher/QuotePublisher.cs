using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace QuoterApp.MarketOrderPublisher;

public class QuotePublisher(IMarketOrderSource orderSource, IServiceScopeFactory serviceScopeFactory, ILogger<QuotePublisher> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(3000, stoppingToken);
        while (true)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var publisher = scope.ServiceProvider.GetService<IPublishEndpoint>();
            
            var order = await orderSource.GetNextMarketOrder();
            logger.LogInformation("Publishing quote: {qoute}", order);
            await publisher.Publish(order, stoppingToken);
        }
        // ReSharper disable once FunctionNeverReturns
    }
}