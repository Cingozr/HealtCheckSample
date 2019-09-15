using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealtCheck.Web.HealthExampleHelpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace HealtCheck.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddHealthChecks()
                .AddCheck("denemelik", () => HealthCheckResult.Healthy("Denemeler Healthy"), tags: new[] { "deneme_Healthy_tag" })
                .AddCheck("denemelik1", () => HealthCheckResult.Unhealthy("Denemeler Unhealthy"), tags: new[] { "deneme_Unhealthy_tag" })
                .AddCheck("denemelik2", () => HealthCheckResult.Degraded("Denemeler Degraded"), tags: new[] { "deneme_Degraded_tag" });
            services.AddHealthChecksUI();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHealthChecks("/working", new HealthCheckOptions
            {
                ResponseWriter = async (c, r) =>
                {
                    c.Response.ContentType = "application/json";
                    var result = JsonConvert.SerializeObject(new
                    {
                        status = r.Status.ToString(),
                        errors = r.Entries.Select(e => new { key = e.Key, value = e.Value.Status.ToString() })
                    });

                    await c.Response.WriteAsync(result);

                }
            });
            app.UseHealthChecksUI(setup =>
            {
                setup.UIPath = "/HealtAdmin";
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
