using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using Research.Implement;
using Research.Interface;
using ResearchWeb.Common;
using ResearchWeb.Models;

namespace Research2._0
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);
            //thuan.nguyen add
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //end add
            services.AddMvc();
            //Thuan.Nguyen add
            services.Add(new ServiceDescriptor(typeof(DBContext), new DBContext(Configuration.GetConnectionString("DefaultConnection"))));
            //services.AddSingleton<IRepositories, Repositories>();

            //services.Add(new ServiceDescriptor(typeof(IDbConnection), new MySqlConnection(Configuration.GetConnectionString("DefaultConnection"))));

            //services.AddTransient<IRepositories, Repositories>();
            //services.Add(new ServiceDescriptor(typeof(IRepositories), new Repositories(Configuration.GetConnectionString("DefaultConnection"))));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //    app.UseBrowserLink();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            //app.UseStaticFiles();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                //
                routes.MapRoute(
                    name: "mchi_tiet",
                    template: "m.{TypeName}/{Alias}/{ID}.html",
                    defaults: new { controller = "mtinnhanh247", action = "chitiet" });
                routes.MapRoute(
                    name: "mdanh_muc",
                    template: "m.{Alias}-{ID}.html",
                    defaults: new { controller = "mtinnhanh247", action = "danhmuc" });
                routes.MapRoute(
                    name: "mdefault",
                    template: "{controller=mtinnhanh247}/{action=trangchu}");

                routes.MapRoute(
                    name: "chi_tiet",
                    template: "{TypeName}/{Alias}/{ID}.html",
                    defaults: new { controller = "tinnhanh247", action = "chitiet" });
                routes.MapRoute(
                    name: "danh_muc",
                    template: "{Alias}-{ID}.html",
                    defaults: new { controller = "tinnhanh247", action = "danhmuc" });
               

                routes.MapRoute(
                    name: "default",
                    template: "{controller=tinnhanh247}/{action=trangchu}");
                

            });
            WebHelpers.Configure(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());


        }
    }
}
