﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TradersMarketplace.Models;
using System.Data.Entity;

namespace TradersMarketplace.DAL
{
    public class ProductRepository : IProductRepository, IDisposable
    {
         private ProductDBContext context;

         public ProductRepository(ProductDBContext context)
        {
            this.context = context;
        }

         public IEnumerable<Product> GetProducts()
        {
            return context.Products.ToList();
        }

         public Product GetProductByID(int? id)
        {
            return context.Products.Find(id);
        }

         public void InsertProduct(Product product)
        {
            context.Products.Add(product);
        }

        public void DeleteProduct(int productID)
        {
            Product student = context.Products.Find(productID);
            context.Products.Remove(student);
        }

        public void UpdateProduct(Product product)
        {
            context.Entry(product).State = EntityState.Modified;
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