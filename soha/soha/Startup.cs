using Core.Infrastructure;
using Microsoft.Owin;
using Owin;
using soha.Config;
using System.Web.Mvc;
using System.Web.Routing;

[assembly: OwinStartupAttribute(typeof(soha.Startup))]
namespace soha
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //AreaRegistration.RegisterAllAreas();
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);

            DependencyRegistrar dp = new DependencyRegistrar();
            dp.Register(null);
            ConfigureAuth(app);
        }
    }
}
