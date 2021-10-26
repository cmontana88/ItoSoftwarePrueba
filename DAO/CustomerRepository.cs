
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
    public class CustomerRepository : ICustomerRepository, IDisposable
    {
        private Model1 context;

        public CustomerRepository(string conexion)
        {
            this.context = new Model1(conexion);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return context.Customer.ToList();
        }

        public Customer GetCustomerByID(int id)
        {
            return context.Customer.Find(id);
        }

        public void InsertCustomer(Customer Customer)
        {
            context.Customer.Add(Customer);
        }

        public void DeleteCustomer(int CustomerID)
        {
            Customer Customer = context.Customer.Find(CustomerID);
            context.Customer.Remove(Customer);
        }

        public void UpdateCustomer(Customer Customer)
        {
            context.Entry(Customer).State = EntityState.Modified;
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
