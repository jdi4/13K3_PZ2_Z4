using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KKBusWebApp.Startup))]
namespace KKBusWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
