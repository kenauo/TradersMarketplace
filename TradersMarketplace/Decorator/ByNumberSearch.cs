using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TradersMarketplace.Models;

namespace TradersMarketplace.Decorator
{
    public class ByNumberSearch : AdvancedSearchDecorator
    {
        public ByNumberSearch() { }
        public ByNumberSearch(string searchIn)
            : base(searchIn) { }

        public Decimal? SearchNumber
        {
            get;
            set;
        }

        public string SearchType
        {
            get;
            set;
        }

        public override List<Product> Search(List<Product> data)
        {
            List<Product> result = data;
            if (SearchNumber != null)
            {
                if (searchIn == "Quantity")
                {
                    switch (SearchType)
                    {
                        case ">":
                            result = data.Where(x => x.Quantity > SearchNumber).ToList<Product>();
                            break;
                        case "=":
                            result = data.Where(x => x.Quantity == SearchNumber).ToList<Product>();
                            break;
                        case "<":
                            result = data.Where(x => x.Quantity < SearchNumber).ToList<Product>();
                            break;
                        default:
                            break;
                    }
                }
                if (searchIn == "Price")
                {
                    switch (SearchType)
                    {
                        case ">":
                            result = data.Where(x => x.Price > SearchNumber).ToList<Product>();
                            break;
                        case "=":
                            result = data.Where(x => x.Price == SearchNumber).ToList<Product>();
                            break;
                        case "<":
                            result = data.Where(x => x.Price < SearchNumber).ToList<Product>();
                            break;
                        default:
                            break;
                    }
                }
            }
            


            if (AdvancedSearchComponent != null)
            {
                result = AdvancedSearchComponent.Search(result);
            }

            return result;
        }

    }
}