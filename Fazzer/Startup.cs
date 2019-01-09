using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fazzer.Startup))]
namespace Fazzer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
