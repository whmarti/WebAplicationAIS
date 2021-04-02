using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LuxuryShop.Startup))]
namespace LuxuryShop
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
