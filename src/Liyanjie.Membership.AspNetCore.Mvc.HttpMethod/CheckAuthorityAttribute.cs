using System;
using System.Linq;

using Liyanjie.Membership.Core;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Liyanjie.Membership.AspNetCore.Mvc.HttpMethod
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

            if (context.HttpContext.Request.Method switch
            {
                "HEAD" => true,
                "OPTIONS" => true,
                _ => false
            })
                return;

            var membership = context.HttpContext.RequestServices.GetRequiredService<Membership<AuthorityProvider>>();
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
            var resource = $"{method}:{(context.ActionDescriptor as ControllerActionDescriptor).ControllerTypeInfo.FullName}".ToUpper();
            if (!membership.AuthorizedAny(new AuthorizationContext(context.HttpContext), resource))
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
}
