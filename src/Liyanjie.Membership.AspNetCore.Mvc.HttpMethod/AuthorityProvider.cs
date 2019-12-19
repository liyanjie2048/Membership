using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Liyanjie.Membership.Core;

using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;

#if NETSTANDARD2_0
using HttpMethodActionConstraint = Microsoft.AspNetCore.Mvc.Internal.HttpMethodActionConstraint;
#endif
#if NETCOREAPP3_0
using HttpMethodActionConstraint = Microsoft.AspNetCore.Mvc.ActionConstraints.HttpMethodActionConstraint;
#endif

namespace Liyanjie.Membership.AspNetCore.Mvc.HttpMethod
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthorityProvider : IAuthorityProvider
    {
        readonly IActionDescriptorCollectionProvider actionDescriptorCollectionProvider;
        readonly AuthorityOptions<AuthorityProvider> options;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionDescriptorCollectionProvider"></param>
        /// <param name="options"></param>
        public AuthorityProvider(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider,
            IOptions<AuthorityOptions<AuthorityProvider>> options)
        {
            this.actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
            this.options = options.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<IAuthority> GetAuthorities()
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
            switch (method)
            {
                case "POST":
                case "PUT":
                case "DELETE":
                    return new[] { "GET" };
                default:
                    return null;
            }
        }
    }
}
