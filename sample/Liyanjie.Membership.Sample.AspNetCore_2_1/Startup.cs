using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Liyanjie.Membership.Sample.AspNetCore_2_1
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("settings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"settings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            Liyanjie.Membership.AspNetCore.Mvc.ActionPath.ServiceCollectionExtensions
                .AddMembership<Liyanjie.Membership.AspNetCore.Mvc.ActionPath.AuthorityProvider, ActionPathMembership>(services, options =>
                {
                    options.Filter = _ => _.FilterDescriptors.Any(__ => __.Filter.GetType() == typeof(ActionPathAuthorityAttribute));
                });
            Liyanjie.Membership.AspNetCore.Mvc.HttpMethod.ServiceCollectionExtensions
                .AddMembership<Liyanjie.Membership.AspNetCore.Mvc.HttpMethod.AuthorityProvider, HttpMethodMembership>(services, options =>
                {
                    options.Filter = _ => _.FilterDescriptors.Any(__ => __.Filter.GetType() == typeof(HttpMethodAuthorityAttribute));
                    options.AuthorityDisplay = new Membership.AspNetCore.Mvc.HttpMethod.AuthorityDisplay
                    {
                        Delete = "删",
                        Get = "查",
                        Post = "增",
                        Put = "改",
                    };
                });
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvcWithDefaultRoute();
        }
    }
}
