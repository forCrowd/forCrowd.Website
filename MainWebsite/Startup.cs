using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MainWebsite.Startup))]
namespace MainWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
