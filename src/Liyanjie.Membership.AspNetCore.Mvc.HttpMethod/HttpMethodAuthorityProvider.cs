using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;

namespace Liyanjie.Membership
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpMethodAuthorityProvider : IAuthorityProvider
    {
        readonly IActionDescriptorCollectionProvider actionDescriptorCollectionProvider;
        readonly HttpMethodAuthorityOptions options;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionDescriptorCollectionProvider"></param>
        /// <param name="options"></param>
        public HttpMethodAuthorityProvider(
            IActionDescriptorCollectionProvider actionDescriptorCollectionProvider,
            IOptions<HttpMethodAuthorityOptions> options)
        {
            this.actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
            this.options = options.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<Authority> GetAuthorities()
        {
            var authorities = new List<Authority>();
            var type_AllowAnonymousFilter = typeof(AllowAnonymousFilter);
            var type_HttpMethodActionConstraint = typeof(HttpMethodActionConstraint);
            var actionDescriptors = actionDescriptorCollectionProvider.ActionDescriptors.Items.ToList();
            if (options.Filter != null)
                actionDescriptors = actionDescriptors.Where(_ => options.Filter(_)).ToList();
            foreach (var item in actionDescriptors)
            {
                var allowAnonymous = item.FilterDescriptors?.Any(_ => _.Filter.GetType() == type_AllowAnonymousFilter) ?? false;
                if (allowAnonymous)
                    continue;

                var controllerActionDescriptor = item as ControllerActionDescriptor;
                var controller = controllerActionDescriptor.ControllerTypeInfo.FullName;
                var group = string.Join("\\", controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes<AuthorityGroupAttribute>(true)?.Reverse()?.Select(_ => _.GroupName) ?? new string[0]);
                var methods = item.ActionConstraints?
                    .Where(_ => _.GetType() == type_HttpMethodActionConstraint)
                    .SelectMany(_ => (_ as HttpMethodActionConstraint)?.HttpMethods)
                    .Distinct();
                if (methods == null)
                    continue;
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
            return method switch
            {
                "POST" or "PUT" or "DELETE" => new[] { "GET" },
                _ => null,
            };
        }
    }
}
