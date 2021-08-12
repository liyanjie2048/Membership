using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Routing;

using Microsoft.Extensions.DependencyInjection;

namespace Liyanjie.Membership.Sample.AspNet
{
    public class Global : HttpApplication
    {
        readonly IServiceCollection services = new ServiceCollection();
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            services.AddMembership<MembershipForMvc, ActionPathAuthorityProvider>();
            services.AddSingleton<ActionPathAuthorityProvider>(serviceProvider => new(
                new[] { "Liyanjie.Membership.Sample.AspNet" },
                ActivatorUtilities.GetServiceOrCreateInstance<ActionPathAuthorityOptions>(serviceProvider)));
            services.AddMembership<MembershipForApi, HttpMethodAuthorityProvider>(options =>
            {
                options.AuthorityDisplay = new()
                {
                    Get = "查看",
                    Post = "添加",
                    Put = "修改",
                    Delete = "删除",
                };
            });
            services.AddSingleton<HttpMethodAuthorityProvider>(serviceProvider => new(
                GlobalConfiguration.Configuration.Services.GetApiExplorer(),
                ActivatorUtilities.GetServiceOrCreateInstance<HttpMethodAuthorityOptions>(serviceProvider)));

            var resolver = new MyDependencyResolver(services.BuildServiceProvider().CreateScope());

            GlobalConfiguration.Configuration.DependencyResolver = resolver;
            DependencyResolver.SetResolver(resolver);

        }
        void Application_EndRequest(object sender, EventArgs e)
        {
            MyDependencyResolver.DisposeServiceScope();
        }

        class MyDependencyResolver : System.Web.Mvc.IDependencyResolver, System.Web.Http.Dependencies.IDependencyResolver
        {
            readonly IServiceScope scope;
            public MyDependencyResolver(IServiceScope scope)
            {
                this.scope = scope;
            }

            public IServiceProvider ServiceProvider
            {
                get
                {
                    if (HttpContext.Current.Items["ServiceScope"] is not IServiceScope scope)
                        HttpContext.Current.Items["ServiceScope"] = scope = this.scope.ServiceProvider.CreateScope();
                    return scope.ServiceProvider;
                }
            }

            public void Dispose()
            {
                scope?.Dispose();
            }

            public object GetService(Type serviceType)
            {
                if (typeof(IController).IsAssignableFrom(serviceType) || typeof(IHttpController).IsAssignableFrom(serviceType))
                {
                    return ActivatorUtilities.CreateInstance(ServiceProvider, serviceType);
                }

                return ServiceProvider.GetService(serviceType);
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                return ServiceProvider.GetServices(serviceType);
            }

            public IDependencyScope BeginScope()
            {
                var scope = this.scope.ServiceProvider.CreateScope();
                return new MyDependencyResolver(scope);
            }

            internal static void DisposeServiceScope()
            {
                if (HttpContext.Current.Items["ServiceScope"] is IServiceScope scope)
                {
                    scope.Dispose();
                }
            }
        }
    }
}