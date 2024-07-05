using System;
using System.Collections.Generic;
using System.Linq;
using QuoterApp.Storage;

namespace QuoterApp.Quoter
{
    public class YourQuoter(IInMemoryRepository inMemoryRepository) : IQuoter
    {
        public double GetQuote(string instrumentId, int quantity)
        {
            var sortedList = inMemoryRepository.ListMarkerOrders(instrumentId);

            if (sortedList.Count == 0)
            {
                throw new KeyNotFoundException($"Quotes with instrumentId: {instrumentId} not found");
            }

            var calculatedPrice = sortedList.Aggregate(new { TotalPrice = 0.0, TotalQuantity = 0 }, (aggregate, marketOrder) =>
            {
                if (aggregate.TotalQuantity >= quantity)
                    return aggregate;

                var takeQuantity = Math.Min(marketOrder.Value.Quantity, quantity - aggregate.TotalQuantity);
                return new
                {
                    TotalPrice = aggregate.TotalPrice + takeQuantity * marketOrder.Value.Price,
                    TotalQuantity = aggregate.TotalQuantity + takeQuantity
                };
            });

            if (calculatedPrice.TotalQuantity < quantity)
            {
                throw new ArgumentException("Not enough quantity in stock.");
            }

            return calculatedPrice.TotalPrice;
        }

        public double GetVolumeWeightedAveragePrice(string instrumentId)
        {
            var sortedList = inMemoryRepository.ListMarkerOrders(instrumentId);

            if (sortedList.Count == 0)
            {
                throw new KeyNotFoundException($"Quotes with instrumentId: {instrumentId} not found");
            }

            double totalVolume = sortedList.Sum(q => q.Value.Quantity);
            double totalVolumePrice = sortedList.Sum(q => q.Value.Price * q.Value.Quantity);

            return totalVolume > 0 ? totalVolumePrice / totalVolume : 0;
        }
    }
}
