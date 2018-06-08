using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebDev.AspNET.Startup))]
namespace WebDev.AspNET
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
