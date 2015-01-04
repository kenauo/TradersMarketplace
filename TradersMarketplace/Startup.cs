using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TradersMarketplace.Startup))]
namespace TradersMarketplace
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
