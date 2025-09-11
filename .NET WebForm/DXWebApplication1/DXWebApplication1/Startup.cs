using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(DXWebApplication1.Startup))]

// Files related to ASP.NET Identity duplicate the Microsoft ASP.NET Identity file structure and contain initial Microsoft comments.

namespace DXWebApplication1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}