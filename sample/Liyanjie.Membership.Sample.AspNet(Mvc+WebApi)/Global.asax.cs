using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Liyanjie.Membership.Core;

namespace Liyanjie.Membership.Sample.AspNet
{
    public class Global : HttpApplication
    {
        public static Membership<Liyanjie.Membership.AspNet.Mvc.ActionPath.AuthorityProvider> ActionPathMembership { get; private set; }
        public static Membership<Liyanjie.Membership.AspNet.WebApi.HttpMethod.AuthorityProvider> HttpMethodMembership { get; private set; }

        void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configure(WebApiConfig.Register);

            ActionPathMembership = new ActionPathMembership(
                new Liyanjie.Membership.AspNet.Mvc.ActionPath.AuthorityProvider(
                    new[] { "Liyanjie.Membership.Sample.AspNet" },
                    new AuthorityOptions<Liyanjie.Membership.AspNet.Mvc.ActionPath.AuthorityProvider, Type>
                    {
                        Filter = _ => _.GetCustomAttributes<ActionPathAuthorityAttribute>(true).Any()
                    }
                )
            );

            var apiExplorer = GlobalConfiguration.Configuration.Services.GetApiExplorer();
            HttpMethodMembership = new HttpMethodMembership(
                new Liyanjie.Membership.AspNet.WebApi.HttpMethod.AuthorityProvider(
                    apiExplorer,
                    new Liyanjie.Membership.AspNet.WebApi.HttpMethod.AuthorityOptions<Liyanjie.Membership.AspNet.WebApi.HttpMethod.AuthorityProvider>
                    {
                        Filter = _ => _.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<HttpMethodAuthorityAttribute>(true).Any(),
                        AuthorityDisplay = new Liyanjie.Membership.AspNet.WebApi.HttpMethod.AuthorityDisplay
                        {
                            Delete = "删除",
                            Get = "查看",
                            Post = "添加",
                            Put = "修改",
                        }
                    }
                )
            );
        }
    }
}