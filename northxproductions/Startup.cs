using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(northxproductions.Startup))]
namespace northxproductions
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
