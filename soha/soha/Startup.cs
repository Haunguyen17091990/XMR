using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(soha.Startup))]
namespace soha
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
