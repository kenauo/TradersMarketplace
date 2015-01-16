using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TradersMarketplace.Models;
using Microsoft.AspNet.Identity;
using TradersMarketplace.DAL;
using TradersMarketplace.Decorator;

namespace TradersMarketplace.Controllers
{
    public class ProductsController : Controller
    {
        private IProductRepository productRepository;

        public ProductsController()
        {
            this.productRepository = new ProductRepository(new ProductDBContext());
        }

        public ProductsController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        // GET: Products
        public ActionResult Index(String Name, String Description, 
            int? Quantity, string QuantityType, Decimal? Price, string PriceType)
        {
            IEnumerable<Product> products = productRepository.GetProducts();
            List<Product> productList = new List<Product>();
            productList.AddRange(products);

            ViewBag.QuantityType = new SelectList(new string[]{">", "=", "<"});
            ViewBag.PriceType = new SelectList(new string[] { ">", "=", "<" });

            AdvancedSearchDecorator search =
                new ByStringSearch("Name")
                {
                    SearchString = Name,
                    AdvancedSearchComponent =
                    new ByStringSearch("Description")
                    {
                        SearchString = Description,
                        AdvancedSearchComponent = new ByNumberSearch("Quantity")
                        {
                            SearchNumber = Quantity,
                            SearchType = QuantityType,
                            AdvancedSearchComponent = new ByNumberSearch("Price") { 
                                SearchNumber = Price,
                                SearchType = PriceType
                            }
                        }
                    }
                };

            return View(search.Search(productList));
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productRepository.GetProductByID(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [Authorize(Roles = "Admin, Seller")]
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Seller")]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Quantity,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                productRepository.InsertProduct(product);
                productRepository.Save();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        [Authorize(Roles = "Admin, Seller")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productRepository.GetProductByID(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Seller")]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Quantity,Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                productRepository.UpdateProduct(product);
                productRepository.Save();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [Authorize(Roles = "Admin, Seller")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = productRepository.GetProductByID(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Seller")]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = productRepository.GetProductByID(id);
            productRepository.DeleteProduct(id);
            productRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                productRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
