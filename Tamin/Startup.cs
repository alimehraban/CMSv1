using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tamin.Startup))]
namespace Tamin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
