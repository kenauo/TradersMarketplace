using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradersMarketplace.DAL;
using TradersMarketplace.Models;

namespace TradersMarketplace.Tests.Controllers
{
    [TestClass]
    public class ProductsControllerTest
    {
        ProductRepository productRepository;

        [TestInitialize]
        public void Initialize()
        {
            productRepository = new ProductRepository(new ProductDBContext());
            productRepository.Context.Database.BeginTransaction();
        }

        [TestCleanup]
        public void CleanUp()
        {
            productRepository.Context.Database.CurrentTransaction.Rollback();
        }

        [TestMethod]
        public void Create()
        {
            List<Product> allProductsBefore = new List<Product>();
            IEnumerable<Product> allProductsBeforeRaw = productRepository.GetProducts();

            for (int i = 0; i < allProductsBeforeRaw.Count(); i++)
            {
                allProductsBefore.Add(allProductsBeforeRaw.ElementAt(i));
            }

            Product newProduct = new Product() { Name = "Smash", Description = "Awesome game.", Quantity = 3, Price = 49.99M };
            Product addedProduct = productRepository.InsertProduct(newProduct);
            productRepository.Save();

            List<Product> expectedProductsAfter = new List<Product>();
            expectedProductsAfter.AddRange(allProductsBefore);

            Product expectedNewProduct = new Product() { ID = addedProduct.ID, Name = "Smash", Description = "Awesome game.", Quantity = 3, Price = 49.99M };
            expectedProductsAfter.Add(expectedNewProduct);

            List<Product> actualProductsAfter = new List<Product>();
            IEnumerable<Product> actualProductsAfterRaw = productRepository.GetProducts();

            for (int i = 0; i < actualProductsAfterRaw.Count(); i++)
            {
                actualProductsAfter.Add(actualProductsAfterRaw.ElementAt(i));
            }

            // compare products before with products after...
            AreListEqual<Product>(expectedProductsAfter, actualProductsAfter);

            // compare new product with the added product...
            Assert.AreEqual<Product>(expectedNewProduct, addedProduct);
        }

        [TestMethod]
        public void Read()
        {
            //To implement later
        }

        [TestMethod]
        public void Update()
        {
            //To implement later
        }

        [TestMethod]
        public void Delete()
        {
            //To implement later
        }

        public static void AreListEqual<T>(List<T> expected, List<T> actual)
        {
            if (actual.Count != expected.Count)
            {
                Assert.Fail("expected and actual lists are not of the same size");
            }

            for (int i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual<T>(expected[i], actual[i]);
            }
        }
    }
}
