using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BSDKhulna.Startup))]
namespace BSDKhulna
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
