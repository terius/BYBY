using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BYBYApp.Startup))]
namespace BYBYApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
