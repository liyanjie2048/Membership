using System;
using System.Linq;
using System.Reflection;
using Liyanjie.Membership.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Liyanjie.Membership.AspNetCore.Mvc.HttpMethod
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

            if (context.HttpContext.Request.Method == "HEAD" || context.HttpContext.Request.Method == "OPTIONS")
                return;

            var membership = context.HttpContext.RequestServices.GetRequiredService<IMembership<AuthorityProvider>>();

            if (membership.IsSuperUser(new AuthorizationContext(context.HttpContext)))
                return;

            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var method = context.HttpContext.Request.Method;
            if ("PATCH".Equals(method, StringComparison.OrdinalIgnoreCase))
                method = "PUT";
            var resource = $"{method}:{controllerActionDescriptor.ControllerTypeInfo.FullName}";
            if (!membership.AuthorizedAny(new AuthorizationContext(context.HttpContext), resource))
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
