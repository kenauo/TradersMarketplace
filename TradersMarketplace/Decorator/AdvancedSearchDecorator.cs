using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TradersMarketplace.Decorator
{
    public abstract class AdvancedSearchDecorator : AdvancedSearchComponent
    {
        public AdvancedSearchComponent AdvancedSearchComponent
        {
            get;
            set;
        }

        public string searchIn;

        public AdvancedSearchDecorator()
        { }

        public AdvancedSearchDecorator(string searchIn)
        {
            this.searchIn = searchIn;
        }

    }
}