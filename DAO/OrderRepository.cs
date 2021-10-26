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
    public class OrderRepository : IOrderRepository, IDisposable
    {
        private Model1 context;

        public OrderRepository(string conexion)
        {
            this.context = new Model1(conexion);
        }

        public IEnumerable<Order> GetOrders()
        {
            return context.Order.ToList();
        }

        public int GetTotalOrder()
        {
            return context.Order.Count();
        }            

        public Order GetOrderByID(int id)
        {
            return context.Order.Find(id);
        }

        public void InsertOrder(Order order)
        {
            context.Entry(order).State = EntityState.Added;
        }

        public void DeleteOrder(int OrderID)
        {
            Order order = context.Order.Find(OrderID);
            context.Order.Remove(order);
        }

        public void UpdateOrder(Order order)
        {
            context.Entry(order).State = EntityState.Modified;
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
