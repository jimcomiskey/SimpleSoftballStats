using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleSoftballStats.Startup))]
namespace SimpleSoftballStats
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
