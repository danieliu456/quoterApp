using System.Threading.Tasks;
using QuoterApp.Contracts;

namespace QuoterApp.MarketOrderPublisher
{
    /// <summary>
    /// Interface to access market orders
    /// </summary>
    public interface IMarketOrderSource
    {
        /// <summary>
        /// Blocking method that will return next available market order.
        /// </summary>
        /// <returns>Market order containing InstrumentId, Price and Quantity</returns>
        public Task<MarketOrder> GetNextMarketOrder();
    }
}
