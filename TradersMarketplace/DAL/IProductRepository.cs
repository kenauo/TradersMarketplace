using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TradersMarketplace.Models;

namespace TradersMarketplace.DAL
{
    public interface IProductRepository : IDisposable
    {
        IEnumerable<Product> GetProducts();
        Product GetProductByID(int? productId);
        void InsertProduct(Product product);
        void DeleteProduct(int productID);
        void UpdateProduct(Product product);
        void Save();
    }
}