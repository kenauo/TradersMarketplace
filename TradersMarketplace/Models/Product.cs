using System.Data.Entity;

namespace TradersMarketplace.Models
{
    public class Product
    {
        public int ID { get; set; }
        public int SellerID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

    public class ProductDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}