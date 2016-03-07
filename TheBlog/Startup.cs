using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TheBlog.Startup))]
namespace TheBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
