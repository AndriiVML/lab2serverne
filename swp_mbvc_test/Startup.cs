using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(swp_mbvc_test.Startup))]
namespace swp_mbvc_test
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
