using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCprojekt.Startup))]
namespace MVCprojekt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
