using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TradersMarketplace.Models
{
    public class Product
    {
        public int ID { get; set; }
        [Display(Name="Seller"), StringLength(256)]
        public string SellerID { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }

    public class ProductDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}