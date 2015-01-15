namespace TradersMarketplace.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TradersMarketplace.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TradersMarketplace.Models.ProductDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "TradersMarketplace.Models.ProductDBContext";
        }

        protected override void Seed(TradersMarketplace.Models.ProductDBContext context)
        {
            var products = new List<Product>();
            for (int i = 0; i < 100; i++)
            {
                products.Add(new Product(){
                    Name = "TestName" + (i + 1),
                    Description = "This is test data " + (i + 1),
                    Quantity = 100 - i,
                    Price = (decimal)(i + 1)/ 100
                });
            }
            products.ForEach(s => context.Products.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();
        }
    }
}
