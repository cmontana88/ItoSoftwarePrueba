using AccessData;
using DAO.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class OrderItemRepository : IOrderItemRepository, IDisposable
    {
        private Model1 context;

        public OrderItemRepository(string conexion)
        {
            this.context = new Model1(conexion);
        }

        public IEnumerable<OrderItem> GetOrderItems()
        {
            return context.OrderItem.ToList();
        }

        public OrderItem GetOrderItemByID(int id)
        {
            return context.OrderItem.Find(id);
        }

        public void InsertOrderItem(OrderItem orderItem)
        {
            context.Entry(orderItem).State = EntityState.Added;
        }

        public void DeleteOrderItem(int OrderItemID)
        {
            OrderItem orderItem = context.OrderItem.Find(OrderItemID);
            context.OrderItem.Remove(orderItem);
        }

        public void UpdateOrderItem(OrderItem orderItem)
        {
            context.Entry(orderItem).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
