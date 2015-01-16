using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TradersMarketplace.Models;

namespace TradersMarketplace.Decorator
{
    public class ByStringSearch : AdvancedSearchDecorator
    {
        public ByStringSearch() { }
        public ByStringSearch(string searchIn)
            : base(searchIn) { }

        public string SearchString
        {
            get;
            set;
        }

        public override List<Product> Search(List<Product> data)
        {
            List<Product> result = data;
            if (!String.IsNullOrWhiteSpace(SearchString))
            {
                if (searchIn == "Name")
                {
                    result = data.Where(x => x.Name.Contains(SearchString)).ToList<Product>();
                }
                if (searchIn == "Description")
                {
                    result = data.Where(x => x.Description.Contains(SearchString)).ToList<Product>();
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
