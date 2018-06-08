using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebDev.AspNETMVC.Startup))]
namespace WebDev.AspNETMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
