using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace Liyanjie.Membership
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpMethodAuthorityProvider : IAuthorityProvider
    {
        readonly IApiExplorer apiExplorer;
        readonly HttpMethodAuthorityOptions options;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiExplorer"></param>
        /// <param name="options"></param>
        public HttpMethodAuthorityProvider(
            IApiExplorer apiExplorer, 
            HttpMethodAuthorityOptions options)
        {
            this.apiExplorer = apiExplorer;
            this.options = options;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<Authority> GetAuthorities()
        {
            var authorities = new List<Authority>();

            var apiDescriptions = apiExplorer.ApiDescriptions.ToList();
            if (options?.Filter != null)
                apiDescriptions = apiDescriptions.Where(_ => options.Filter(_)).ToList();
            foreach (var item in apiDescriptions)
            {
                if (item.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>(true).Any()
                    || item.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>(true).Any())
                    continue;

                var controller = item.ActionDescriptor.ControllerDescriptor.ControllerType.FullName;
                var group = string.Join("\\", item.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<AuthorityGroupAttribute>(true).Reverse().Select(_ => _.GroupName));
                var methods = item.ActionDescriptor.SupportedHttpMethods.Select(_ => _.Method).Distinct();
                foreach (var m in methods)
                {
                    var method = m;
                    if ("PATCH".Equals(method, StringComparison.OrdinalIgnoreCase))
                        method = "PUT";
                    var resource = $"{method}:{controller}";
                    if (authorities.Any(_ => _.Resource == resource))
                        continue;
                    authorities.Add(new Authority
                    {
                        Group = group,
                        Resource = $"{method}:{controller}",
                        Dependences = Dependences(method),
                        Title = options.AuthorityDisplay?.GetValue(method),
                    });
                }
            }
            return authorities;
        }

        static string[] Dependences(string method)
        {
            switch (method)
            {
                case "POST":
                case "PUT":
                case "PATCH":
                case "DELETE":
                    return new[] { "GET" };
                default:
                    return null;
            }
        }
    }
}
