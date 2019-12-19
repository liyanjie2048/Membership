using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Liyanjie.Membership.Core;

using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;

namespace Liyanjie.Membership.AspNetCore.Mvc.ActionPath
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthorityProvider : IAuthorityProvider
    {
        readonly IActionDescriptorCollectionProvider actionDescriptorCollectionProvider;
        readonly AuthorityOptions<AuthorityProvider, ActionDescriptor> options;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionDescriptorCollectionProvider"></param>
        /// <param name="options"></param>
        public AuthorityProvider(
            IActionDescriptorCollectionProvider actionDescriptorCollectionProvider,
            IOptions<AuthorityOptions<AuthorityProvider, ActionDescriptor>> options)
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
            var actionDescriptors = actionDescriptorCollectionProvider.ActionDescriptors.Items.ToList();
            if (options.Filter != null)
                actionDescriptors = actionDescriptors.Where(_ => options.Filter(_)).ToList();
            foreach (var item in actionDescriptors)
            {
                var allowAnonymous = item.FilterDescriptors?.Any(_ => _.Filter.GetType() == type_AllowAnonymousFilter) ?? false;
                if (allowAnonymous)
                    continue;

                var controllerActionDescriptor = item as ControllerActionDescriptor;
                var authority = controllerActionDescriptor?.MethodInfo.GetCustomAttributes<AuthorityAttribute>().FirstOrDefault();
                if (authority?.Unlisted ?? false)
                    continue;

                var group = string.Join("\\", controllerActionDescriptor?.ControllerTypeInfo.GetCustomAttributes<AuthorityGroupAttribute>(true).Reverse().Select(_ => _.GroupName) ?? new string[0]);
                var resource = item.DisplayName.Remove(item.DisplayName.IndexOf(" ("));
                var dependences = authority?.Dependences
                    .Select(_ => _.StartsWith("Controllers.") ? resource.Remove(resource.IndexOf("Controllers.")) + _ : _.StartsWith(".") ? resource.Remove(resource.LastIndexOf(".")) + _ : string.Empty)
                    .ToArray();
                var title = authority?.Title;

                var exists = authorities.FirstOrDefault(_ => _.Resource == resource);
                if (exists == null)
                    authorities.Add(new Authority
                    {
                        Group = group,
                        Resource = resource,
                        Dependences = dependences,
                        Title = title,
                    });
                else
                {
                    if (exists.Title == null)
                        exists.Title = title;
                    if (exists.Dependences == null)
                        exists.Dependences = dependences;
                }
            }
            return authorities;
        }
    }
}
