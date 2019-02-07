using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProperArch01.WebApp.Startup))]
namespace ProperArch01.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
