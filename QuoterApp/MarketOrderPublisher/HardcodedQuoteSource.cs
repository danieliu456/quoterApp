using System.Threading;
using System.Threading.Tasks;
using QuoterApp.Contracts;

namespace QuoterApp.MarketOrderPublisher
{
    public class HardcodedMarketOrderSource : IMarketOrderSource
    {
        private readonly MarketOrder[] _quotes =
        [
            new() {InstrumentId = "BA79603015", Price = 102.997, Quantity = 12 },
            new() {InstrumentId = "BA79603015", Price = 103.2, Quantity = 60 },
            new() {InstrumentId = "AB73567490", Price = 103.25, Quantity = 79 },
            new() {InstrumentId = "AB73567490", Price = 95.5, Quantity = 14 },
            new() {InstrumentId = "BA79603015", Price = 98.0, Quantity = 1 },
            new() {InstrumentId = "AB73567490", Price = 100.7, Quantity = 17 },
            new() {InstrumentId = "DK50782120", Price = 100.001, Quantity = 900 },
            new() {InstrumentId = "DK50782120", Price = 99.81, Quantity = 100 },
            new() {InstrumentId = "DK50782120", Price = 82.001, Quantity = 88 },
            new() {InstrumentId = "DK50782120", Price = 72.81, Quantity = 72 },
            new() {InstrumentId = "DK50782120", Price = 62.001, Quantity = 62 },
            new() {InstrumentId = "DK50782120", Price = 56.81, Quantity = 52 },
            new() {InstrumentId = "DK50782120", Price = 52.001, Quantity = 32 },
            new() {InstrumentId = "DK50782120", Price = 46.81, Quantity = 12 },
            new() {InstrumentId = "DK50782120", Price = 40.001, Quantity = 14 },
            new() {InstrumentId = "DK50782120", Price = 38.81, Quantity = 17 },
            new() {InstrumentId = "DK50782120", Price = 22.001, Quantity = 18 },
            new() {InstrumentId = "DK50782120", Price = 11.81, Quantity = 15 },
            
        ];

        private int _position = 0;

        public async Task<MarketOrder> GetNextMarketOrder()
        {
            if (_quotes.Length <= _position)
            {
                // No more quotes to give
                await Task.Delay(Timeout.Infinite);
            }

            await Task.Delay(1000); // Simulates delay in getting next quote
            return _quotes[_position++];
        }
    }
}
