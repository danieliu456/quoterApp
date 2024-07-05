using System.Threading.Tasks;
using MassTransit;
using Microsoft.Extensions.Logging;
using QuoterApp.Contracts;
using QuoterApp.Storage;

namespace QuoterApp.MarketOrderConsumer;

public class MarketOrderConsumer(ILogger<MarketOrderConsumer> logger, IInMemoryRepository inMemoryRepository) : IConsumer<MarketOrder>
{
    public Task Consume(ConsumeContext<MarketOrder> context)
    {
        var message = context.Message;
        logger.LogInformation("Received market order {message}", message);
        
        inMemoryRepository.AddOrder(message);
        
        return Task.CompletedTask;
    }
}