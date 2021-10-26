using AccessData;
using System;
using System.Collections.Generic;

namespace DAO.Interfaces
{
    public interface IOrderRepository : IDisposable
    {
        IEnumerable<Order> GetOrders();
        Order GetOrderByID(int OrderId);

        int GetTotalOrder();
        void InsertOrder(Order order);
        void DeleteOrder(int OrderID);
        void UpdateOrder(Order order);
        void Save();
    }
}
