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
    public class ProductRepository : IProductRepository, IDisposable
    {
        private Model1 context;

        public ProductRepository(string conexion)
        {
            this.context = new Model1(conexion);
        }

        public IEnumerable<Product> GetProducts()
        {
            return context.Product.ToList();
        }

        public Product GetProductByID(int id)
        {
            return context.Product.Find(id);
        }

        public void InsertProduct(Product Product)
        {
            context.Product.Add(Product);
        }

        public void DeleteProduct(int ProductID)
        {
            Product Product = context.Product.Find(ProductID);
            context.Product.Remove(Product);
        }

        public void UpdateProduct(Product Product)
        {
            context.Entry(Product).State = EntityState.Modified;
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
