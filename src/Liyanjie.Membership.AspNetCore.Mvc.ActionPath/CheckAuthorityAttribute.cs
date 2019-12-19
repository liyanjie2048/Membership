using System;
using System.Linq;
using System.Reflection;

using Liyanjie.Membership.Core;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Liyanjie.Membership.AspNetCore.Mvc.ActionPath
{
    /// <summary>
    /// 
    /// </summary>
    public class CheckAuthorityAttribute : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public virtual void OnAuthorization(AuthorizationFilterContext context)
        {
#if NETCOREAPP3_0
            var type_IAllowAnonymous = typeof(IAllowAnonymous);
            if (context.ActionDescriptor.EndpointMetadata.Any(_ => type_IAllowAnonymous.IsAssignableFrom(_.GetType())))
                return;
#endif
            var type_AllowAnonymousFilter = typeof(AllowAnonymousFilter);
            if (context.ActionDescriptor.FilterDescriptors.Any(_ => _.Filter.GetType() == type_AllowAnonymousFilter))
                return;

            var membership = context.HttpContext.RequestServices.GetRequiredService<Membership<AuthorityProvider>>();
            if (membership.IsSuperUser(context.HttpContext.User))
                return;

            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            var resource = $"{controllerActionDescriptor.ControllerTypeInfo.FullName}.{controllerActionDescriptor.ActionName}";

            var authority = controllerActionDescriptor.MethodInfo.GetCustomAttribute<AuthorityAttribute>();
            var resources = authority?.References?
                  .Select(_ => _.StartsWith("Controllers.") ? resource.Remove(resource.IndexOf("Controllers.")) + _ : _.StartsWith(".") ? resource.Remove(resource.LastIndexOf(".")) + _ : null)
                  .Where(_ => _ != null)
                  .ToArray();
            if (!membership.AuthorizedAny(context.HttpContext.User, resource, resources))
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
