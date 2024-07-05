using System.Collections.Generic;
using QuoterApp.Contracts;

namespace QuoterApp.Storage;

public interface IInMemoryRepository
{
    void AddOrder(MarketOrder order);
    SortedList<double, MarketOrder>  ListMarkerOrders(string instrumentationId);
}
