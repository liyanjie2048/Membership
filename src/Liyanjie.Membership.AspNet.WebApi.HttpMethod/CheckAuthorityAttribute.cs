using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

using Liyanjie.Membership.Core;

namespace Liyanjie.Membership.AspNet.WebApi.HttpMethod
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class CheckAuthorityAttribute : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        protected abstract Membership<AuthorityProvider> Membership { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="continuation"></param>
        /// <returns></returns>
        public virtual async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>(true).Any()
                || actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>(true).Any())
                return await continuation();

            if (actionContext.Request.Method.Method switch
            {
                "HEAD" => true,
                "OPTIONS" => true,
                _ => false
            })
                return await continuation();

            if (Membership.IsSuperUser(actionContext.RequestContext.Principal))
                return await continuation();

            if (!actionContext.RequestContext.Principal.Identity.IsAuthenticated)
                return actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized!");

            var method = actionContext.Request.Method.Method;
            if ("PATCH".Equals(method, StringComparison.OrdinalIgnoreCase))
                method = "PUT";
            var resource = $"{method}:{actionContext.ActionDescriptor.ControllerDescriptor.ControllerType.FullName}";
            if (Membership.AuthorizedAny(actionContext.RequestContext.Principal, resource))
                return await continuation();

            return actionContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Forbidden!");
        }
    }
}
