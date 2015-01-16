using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TradersMarketplace.Models;

namespace TradersMarketplace.Decorator
{
    public abstract class AdvancedSearchComponent
    {
        public abstract List<Product> Search(List<Product> data);
    }
}