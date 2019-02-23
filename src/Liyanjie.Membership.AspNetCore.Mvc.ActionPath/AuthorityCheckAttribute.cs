using System;
using System.Linq;
using System.Reflection;
using Liyanjie.Membership.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Liyanjie.Membership.AspNetCore.Mvc.ActionPath
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthorityCheckAttribute : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public virtual void OnAuthorization(AuthorizationFilterContext context)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (controllerActionDescriptor.ControllerTypeInfo.GetCustomAttributes<AllowAnonymousAttribute>().Any() || controllerActionDescriptor.MethodInfo.GetCustomAttributes<AllowAnonymousAttribute>().Any())
                return;

            var membership = context.HttpContext.RequestServices.GetRequiredService<IMembership<AuthorityProvider>>();

            if (membership.IsSuperUser(new AuthorizationContext(context.HttpContext)))
                return;

            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var resource = $"{controllerActionDescriptor.ControllerTypeInfo.FullName}.{controllerActionDescriptor.ActionName}";

            var authority = controllerActionDescriptor.MethodInfo.GetCustomAttribute<AuthorityAttribute>();
            var resources = authority?.References?
                  .Select(_ => _.StartsWith("Controllers.") ? resource.Remove(resource.IndexOf("Controllers.")) + _ : _.StartsWith(".") ? resource.Remove(resource.LastIndexOf(".")) + _ : null)
                  .Where(_ => _ != null)
                  .ToArray();
            if (!membership.AuthorizedAny(new AuthorizationContext(context.HttpContext), resource, resources))
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
