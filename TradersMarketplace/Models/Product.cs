using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TradersMarketplace.Models
{
    public class Product
    {
        public int ID { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required, Range(0, int.MaxValue, ErrorMessage= "Quantity must not be negative.")]
        public int Quantity { get; set; }
        [Required, DataType(DataType.Currency),
        Range(0, double.MaxValue, ErrorMessage= "Price must not be negative.")]
        public decimal Price { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Product)
            {
                Product compareProduct = (Product)obj;

                if (//compareProduct.ID != this.ID &&
                    compareProduct.ID != this.ID &&
                    compareProduct.Name == this.Name &&
                    compareProduct.Description == this.Description &&
                    compareProduct.Quantity == this.Quantity &&
                    compareProduct.Price == this.Price)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return base.Equals(obj);
            }
        }
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