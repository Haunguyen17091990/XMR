using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Core.DataAccess.Implement;
using Core.DataAccess.Interface;
using Core.Infrastructure;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using XRM.Business.Service.Common;
namespace soha.Config
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(InfrastructureConfig config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterFilterProvider();
            builder.Register(c => new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
               .As<IDbConnection>().InstancePerLifetimeScope();
            builder.RegisterType<DapperReadOnlyRepository>().As<IReadOnlyRepository>().InstancePerLifetimeScope();
            builder.RegisterType<DapperRepository>().As<IRepository>().InstancePerLifetimeScope();
            builder.RegisterType<NewsService>().As<INewsService>().InstancePerLifetimeScope();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}