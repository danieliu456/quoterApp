using System.Collections.Concurrent;
using System.Collections.Generic;
using QuoterApp.Contracts;

namespace QuoterApp.Storage;

public class InMemoryRepository : IInMemoryRepository
{
    private static readonly ConcurrentDictionary<string, SortedList<double, MarketOrder>> OrderDataset = new();

    public void AddOrder(MarketOrder order)
    {
        OrderDataset.AddOrUpdate(
            order.InstrumentId,
            (key) =>
            {
                var list = new SortedList<double, MarketOrder>();
                list.Add(order.Price, order);
                return list;
            },
            (s, list) =>
            {
                list.Add(order.Price, order);
                return list;
            });
    }

    public SortedList<double, MarketOrder> ListMarkerOrders(string instrumentationId)
    {
        return OrderDataset.GetValueOrDefault(instrumentationId, new SortedList<double, MarketOrder>(0));
    }
}