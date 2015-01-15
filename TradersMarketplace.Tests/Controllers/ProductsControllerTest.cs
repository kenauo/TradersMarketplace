using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradersMarketplace.DAL;
using TradersMarketplace.Models;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;

namespace TradersMarketplace.Tests.Controllers
{
    [TestClass]
    public class ProductsControllerTest
    {
        ProductRepository productRepository;
        Product baseProduct = new Product()
        {
            Name = "NameTest",
            Description = "DescriptionTest",
            Quantity = 1,
            Price = 1.99M
        };
        int existingID = 300, inexistingID = 999, invalidID = -1;

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

        #region Create Name Tests
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void CreateTestName1()
        {
            Product testProduct = baseProduct;
            testProduct.Name = null;
            createTestBase(testProduct);
        }
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void CreateTestName2()
        {
            Product testProduct = baseProduct;
            testProduct.Name = String.Empty;
            createTestBase(testProduct);
        }
        [TestMethod]
        public void CreateTestName3()
        {
            Product testProduct = baseProduct;
            testProduct.Name = "a";
            createTestBase(testProduct);
        }
        [TestMethod]
        public void CreateTestName4()
        {
            Product testProduct = baseProduct;
            testProduct.Name = createStringOfLength(99);
            createTestBase(testProduct);
        }
        [TestMethod]
        public void CreateTestName5()
        {
            Product testProduct = baseProduct;
            testProduct.Name = createStringOfLength(100);
            createTestBase(testProduct);
        }
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void CreateTestName6()
        {
            Product testProduct = baseProduct;
            testProduct.Name = createStringOfLength(101);
            createTestBase(testProduct);
        }
        [TestMethod]
        public void CreateTestName7()
        {
            Product testProduct = baseProduct;
            testProduct.Name = "BjØrn";
            createTestBase(testProduct);
        }
        #endregion
        #region Create Description Tests
        [TestMethod]
        public void CreateTestDescription1()
        {
            Product testProduct = baseProduct;
            testProduct.Description = null;
            createTestBase(testProduct);
        }
        [TestMethod]
        public void CreateTestDescription2()
        {
            Product testProduct = baseProduct;
            testProduct.Description = String.Empty;
            createTestBase(testProduct);
        }
        [TestMethod]
        public void CreateTestDescription3()
        {
            Product testProduct = baseProduct;
            testProduct.Description = "a";
            createTestBase(testProduct);
        }
        [TestMethod]
        public void CreateTestDescription4()
        {
            Product testProduct = baseProduct;
            testProduct.Description = "BjØrn";
            createTestBase(testProduct);
        }
        #endregion
        #region Create Quantity Tests
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void CreateTestQuantity1()
        {
            Product testProduct = baseProduct;
            testProduct.Quantity = -1;
            createTestBase(testProduct);
        }
        [TestMethod]
        public void CreateTestQuantity2()
        {
            Product testProduct = baseProduct;
            testProduct.Quantity = 0;
            createTestBase(testProduct);
        }
        [TestMethod]
        public void CreateTestQuantity3()
        {
            Product testProduct = baseProduct;
            testProduct.Quantity = 1;
            createTestBase(testProduct);
        }
        #endregion
        #region Create Price Tests
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void CreateTestPrice1()
        {
            Product testProduct = baseProduct;
            testProduct.Price = -0.01M;
            createTestBase(testProduct);
        }
        [TestMethod]
        public void CreateTestPrice2()
        {
            Product testProduct = baseProduct;
            testProduct.Price = 0;
            createTestBase(testProduct);
        }
        [TestMethod]
        public void CreateTestPrice3()
        {
            Product testProduct = baseProduct;
            testProduct.Price = 0.01M;
            createTestBase(testProduct);
        }
        #endregion

        [TestMethod]
        [ExpectedException(typeof(DbUpdateConcurrencyException))]
        public void UpdateInexistingTest()
        {
            Product testProduct = new Product()
            {
                ID = inexistingID,
                Name = "NameTest",
                Description = "DescriptionTest",
                Quantity = 1,
                Price = 1.99M
            };
            //Gets all current products.
            List<Product> allProductsBefore = new List<Product>();
            IEnumerable<Product> allProductsBeforeRaw = productRepository.GetProducts();
            for (int i = 0; i < allProductsBeforeRaw.Count(); i++)
            {
                allProductsBefore.Add(allProductsBeforeRaw.ElementAt(i));
            }

            //Tries to edit.
            Product editProduct = testProduct;
            bool exceptionThrown = false;
            try
            {
                productRepository.UpdateProduct(editProduct);
                productRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                exceptionThrown = true;
            }

            //The actual products after are retrieved.
            List<Product> actualProductsAfter = new List<Product>();
            IEnumerable<Product> actualProductsAfterRaw = productRepository.GetProducts();
            for (int i = 0; i < actualProductsAfterRaw.Count(); i++)
            {
                actualProductsAfter.Add(actualProductsAfterRaw.ElementAt(i));
            }


            //Products before and after are compared.
            if (allProductsBefore.Count != actualProductsAfter.Count)
            {
                Assert.Fail("previous and actual lists are not of the same size");
            }

            //Exception thrown at the end so the lists are still compared to be equal.
            if (exceptionThrown)
            {
                throw new DbUpdateConcurrencyException();
            }
        }
        [TestMethod]
        [ExpectedException(typeof(DbUpdateConcurrencyException))]
        public void UpdateInvalidTest()
        {
            Product testProduct = new Product()
            {
                ID = invalidID,
                Name = "NameTest",
                Description = "DescriptionTest",
                Quantity = 1,
                Price = 1.99M
            };
            //Gets all current products.
            List<Product> allProductsBefore = new List<Product>();
            IEnumerable<Product> allProductsBeforeRaw = productRepository.GetProducts();
            for (int i = 0; i < allProductsBeforeRaw.Count(); i++)
            {
                allProductsBefore.Add(allProductsBeforeRaw.ElementAt(i));
            }

            //Tries to edit.
            Product editProduct = testProduct;
            bool exceptionThrown = false;
            try
            {
                productRepository.UpdateProduct(editProduct);
                productRepository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                exceptionThrown = true;
            }

            //The actual products after are retrieved.
            List<Product> actualProductsAfter = new List<Product>();
            IEnumerable<Product> actualProductsAfterRaw = productRepository.GetProducts();
            for (int i = 0; i < actualProductsAfterRaw.Count(); i++)
            {
                actualProductsAfter.Add(actualProductsAfterRaw.ElementAt(i));
            }


            //Products before and after are compared.
            if (allProductsBefore.Count != actualProductsAfter.Count)
            {
                Assert.Fail("previous and actual lists are not of the same size");
            }

            //Exception thrown at the end so the lists are still compared to be equal.
            if (exceptionThrown)
            {
                throw new DbUpdateConcurrencyException();
            }
        }
        #region Update Name Tests
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void UpdateTestName1()
        {
            Product testProduct = productRepository.GetProductByID(existingID);
            testProduct.Name = null;
            updateTestBase(testProduct);
        }
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void UpdateTestName2()
        {
            Product testProduct = productRepository.GetProductByID(existingID);
            testProduct.Name = String.Empty;
            updateTestBase(testProduct);
        }
        [TestMethod]
        public void UpdateTestName3()
        {
            Product testProduct = productRepository.GetProductByID(existingID);
            testProduct.Name = "a";
            updateTestBase(testProduct);
        }
        [TestMethod]
        public void UpdateTestName4()
        {
            Product testProduct = productRepository.GetProductByID(existingID);
            testProduct.Name = createStringOfLength(99);
            updateTestBase(testProduct);
        }
        [TestMethod]
        public void UpdateTestName5()
        {
            Product testProduct = productRepository.GetProductByID(existingID);
            testProduct.Name = createStringOfLength(100);
            updateTestBase(testProduct);
        }
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void UpdateTestName6()
        {
            Product testProduct = productRepository.GetProductByID(existingID);
            testProduct.Name = createStringOfLength(101);
            updateTestBase(testProduct);
        }
        [TestMethod]
        public void UpdateTestName7()
        {
            Product testProduct = productRepository.GetProductByID(existingID);
            testProduct.Name = "BjØrn";
            updateTestBase(testProduct);
        }
        #endregion
        #region Update Description Tests
        [TestMethod]
        public void UpdateTestDescription1()
        {
            Product testProduct = productRepository.GetProductByID(existingID);
            testProduct.Description = null;
            updateTestBase(testProduct);
        }
        [TestMethod]
        public void UpdateTestDescription2()
        {
            Product testProduct = productRepository.GetProductByID(existingID);
            testProduct.Description = String.Empty;
            updateTestBase(testProduct);
        }
        [TestMethod]
        public void UpdateTestDescription3()
        {
            Product testProduct = productRepository.GetProductByID(existingID);
            testProduct.Description = "a";
            updateTestBase(testProduct);
        }
        [TestMethod]
        public void UpdateTestDescription4()
        {
            Product testProduct = productRepository.GetProductByID(existingID);
            testProduct.Description = "BjØrn";
            updateTestBase(testProduct);
        }
        #endregion
        #region Update Quantity Tests
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void UpdateTestQuantity1()
        {
            Product testProduct = productRepository.GetProductByID(existingID);
            testProduct.Quantity = -1;
            updateTestBase(testProduct);
        }
        [TestMethod]
        public void UpdateTestQuantity2()
        {
            Product testProduct = productRepository.GetProductByID(existingID);
            testProduct.Quantity = 0;
            updateTestBase(testProduct);
        }
        [TestMethod]
        public void UpdateTestQuantity3()
        {
            Product testProduct = productRepository.GetProductByID(existingID);
            testProduct.Quantity = 1;
            updateTestBase(testProduct);
        }
        #endregion
        #region Update Price Tests
        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void UpdateTestPrice1()
        {
            Product testProduct = productRepository.GetProductByID(existingID);
            testProduct.Price = -0.01M;
            updateTestBase(testProduct);
        }
        [TestMethod]
        public void UpdateTestPrice2()
        {
            Product testProduct = productRepository.GetProductByID(existingID);
            testProduct.Price = 0;
            updateTestBase(testProduct);
        }
        [TestMethod]
        public void UpdateTestPrice3()
        {
            Product testProduct = productRepository.GetProductByID(existingID);
            testProduct.Price = 0.01M;
            updateTestBase(testProduct);
        }
        #endregion

        #region Delete Tests
        [TestMethod]
        public void DeleteValidTest()
        {
            deleteTestBase(existingID);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteInexistingTest()
        {
            deleteTestBase(inexistingID);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeleteInvalidTest()
        {
            deleteTestBase(invalidID);
        }
        #endregion

        #region Read Tests
        [TestMethod]
        public void ReadValidTest()
        {
            readTestBase(existingID);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadInexistingTest()
        {
            readTestBase(inexistingID);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ReadInvalidTest()
        {
            readTestBase(invalidID);
        }
        #endregion

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

        public string createStringOfLength(int length)
        {
            string testString = "";
            for (int i = 0; i < length; i++)
            {
                testString += "a";
            }
            return testString;
        }

        public void createTestBase(Product product)
        {
            //Gets all current products.
            List<Product> allProductsBefore = new List<Product>();
            IEnumerable<Product> allProductsBeforeRaw = productRepository.GetProducts();
            for (int i = 0; i < allProductsBeforeRaw.Count(); i++)
            {
                allProductsBefore.Add(allProductsBeforeRaw.ElementAt(i));
            }

            //Tries to add a new product.
            Product newProduct = product;
            Product addedProduct = null;
            Product expectedNewProduct = null;
            bool exceptionThrown = false;
            try
            {
                addedProduct = productRepository.InsertProduct(newProduct);
                productRepository.Save();
                expectedNewProduct = new Product() { 
                    ID = addedProduct.ID, 
                    Name = product.Name, 
                    Description = product.Description, 
                    Quantity = product.Quantity, 
                    Price = product.Price 
                };
            }
            catch(DbEntityValidationException)
            {
                exceptionThrown = true;
            }

            //Sets the products that are expected after.
            List<Product> expectedProductsAfter = new List<Product>();
            expectedProductsAfter.AddRange(allProductsBefore);

            //If the product was added successfully, it's added to the expected
            //products after.
            if (expectedNewProduct != null)
            {
                expectedProductsAfter.Add(expectedNewProduct);
            }
            
            //The actual products after are retrieved.
            List<Product> actualProductsAfter = new List<Product>();
            IEnumerable<Product> actualProductsAfterRaw = productRepository.GetProducts();
            for (int i = 0; i < actualProductsAfterRaw.Count(); i++)
            {
                actualProductsAfter.Add(actualProductsAfterRaw.ElementAt(i));
            }


            //Products before and after are compared.
            AreListEqual<Product>(expectedProductsAfter, actualProductsAfter);

            //If the product was added it is compared with the expected product.
            if (expectedNewProduct != null && addedProduct != null)
            {
                Assert.AreEqual<Product>(expectedNewProduct, addedProduct);
            }

            //Exception thrown at the end so the lists are still compared to be equal.
            if (exceptionThrown)
            {
                throw new DbEntityValidationException();
            }
        }
        public void updateTestBase(Product product)
        {
            //Gets all current products.
            List<Product> allProductsBefore = new List<Product>();
            IEnumerable<Product> allProductsBeforeRaw = productRepository.GetProducts();
            for (int i = 0; i < allProductsBeforeRaw.Count(); i++)
            {
                allProductsBefore.Add(allProductsBeforeRaw.ElementAt(i));
            }

            //Tries to edit.
            Product editProduct = product;
            bool exceptionThrown = false;
            try
            {
                productRepository.UpdateProduct(editProduct);
                productRepository.Save();
            }
            catch (DbEntityValidationException)
            {
                exceptionThrown = true;
            }

            //The actual products after are retrieved.
            List<Product> actualProductsAfter = new List<Product>();
            IEnumerable<Product> actualProductsAfterRaw = productRepository.GetProducts();
            for (int i = 0; i < actualProductsAfterRaw.Count(); i++)
            {
                actualProductsAfter.Add(actualProductsAfterRaw.ElementAt(i));
            }


            //Products before and after are compared.
            if (allProductsBefore.Count != actualProductsAfter.Count)
            {
                Assert.Fail("previous and actual lists are not of the same size");
            }

            //Checks that the product was in fact updated.
            //Exception thrown at the end so the lists are still compared to be equal.
            if (!exceptionThrown)
            {
                Product editCheck = productRepository.GetProductByID(product.ID);
                Assert.AreEqual<Product>(editProduct, editCheck);
            }
            else
            {
                throw new DbEntityValidationException();
            }
        }
        public void deleteTestBase(int id)
        {
            //Gets all current products.
            List<Product> allProductsBefore = new List<Product>();
            IEnumerable<Product> allProductsBeforeRaw = productRepository.GetProducts();
            for (int i = 0; i < allProductsBeforeRaw.Count(); i++)
            {
                allProductsBefore.Add(allProductsBeforeRaw.ElementAt(i));
            }

            //Tries to delete.
            bool exceptionThrown = false;
            try
            {
                productRepository.DeleteProduct(id);
                productRepository.Save();
            }
            catch (ArgumentNullException)
            {
                exceptionThrown = true;
            }

            //The actual products after are retrieved.
            List<Product> actualProductsAfter = new List<Product>();
            IEnumerable<Product> actualProductsAfterRaw = productRepository.GetProducts();
            for (int i = 0; i < actualProductsAfterRaw.Count(); i++)
            {
                actualProductsAfter.Add(actualProductsAfterRaw.ElementAt(i));
            }

            //Exception thrown at the end so the lists are still compared to be equal.
            if (exceptionThrown)
            {
                //Products before and after are compared.
                if (allProductsBefore.Count != actualProductsAfter.Count)
                {
                    Assert.Fail("previous and actual lists are not of the same size");
                }
                throw new ArgumentNullException();
            }
            else
            {
                if (allProductsBefore.Count != actualProductsAfter.Count + 1)
                {
                    Assert.Fail("No record has been deleted");
                }
            }
        }
        public void readTestBase(int id)
        {
            //Gets all current products.
            List<Product> allProductsBefore = new List<Product>();
            IEnumerable<Product> allProductsBeforeRaw = productRepository.GetProducts();
            for (int i = 0; i < allProductsBeforeRaw.Count(); i++)
            {
                allProductsBefore.Add(allProductsBeforeRaw.ElementAt(i));
            }

            //Tries to get a product
            bool exceptionThrown = false;
            try
            {
                Product testProduct = productRepository.GetProductByID(id);
            }
            catch (ArgumentNullException)
            {
                exceptionThrown = true;
            }

            //The actual products after are retrieved.
            List<Product> actualProductsAfter = new List<Product>();
            IEnumerable<Product> actualProductsAfterRaw = productRepository.GetProducts();
            for (int i = 0; i < actualProductsAfterRaw.Count(); i++)
            {
                actualProductsAfter.Add(actualProductsAfterRaw.ElementAt(i));
            }

            //Products before and after are compared.
            AreListEqual<Product>(allProductsBefore, actualProductsAfter);

            if (exceptionThrown)
            {
                throw new ArgumentNullException();
            }
        }
    }
}
