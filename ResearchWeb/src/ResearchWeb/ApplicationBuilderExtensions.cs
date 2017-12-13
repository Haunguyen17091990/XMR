using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchWeb
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCustomRewriter(this IApplicationBuilder app)
        {
            var options = new RewriteOptions()
                .AddRewrite("(.*)/(.*)/(.*)", "$1/$2/$3",skipRemainingRules:true);

            return app.UseRewriter(options);
        }
    }
}
