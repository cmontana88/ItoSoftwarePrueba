using AccessData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Interfaces
{
    public interface IOrderItemRepository : IDisposable
    {
        IEnumerable<OrderItem> GetOrderItems();
        OrderItem GetOrderItemByID(int OrderItemId);
        void InsertOrderItem(OrderItem orderItem);
        void DeleteOrderItem(int OrderItemID);
        void UpdateOrderItem(OrderItem orderItem);
        void Save();
    }
}
