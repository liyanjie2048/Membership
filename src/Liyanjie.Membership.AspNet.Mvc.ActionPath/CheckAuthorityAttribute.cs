using System.Linq;
using System.Net;
using System.Web.Mvc;

using Liyanjie.Membership.Core;

namespace Liyanjie.Membership.AspNet.Mvc.ActionPath
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
        /// <param name="filterContext"></param>
        public virtual void OnAuthorization(AuthorizationContext filterContext)
        {
            var actionDescriptor = filterContext.ActionDescriptor;

            var type_AllowAnonymousAttribute = typeof(AllowAnonymousAttribute);
            if (actionDescriptor.ControllerDescriptor.IsDefined(type_AllowAnonymousAttribute, true)
                || actionDescriptor.IsDefined(type_AllowAnonymousAttribute, true))
                return;

            if (Membership.IsSuperUser(filterContext.HttpContext.User))
                return;

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
                return;
            }

            var resource = $"{actionDescriptor.ControllerDescriptor.ControllerType.FullName}.{actionDescriptor.ActionName}";
            var authority = actionDescriptor.GetCustomAttributes(typeof(AuthorityAttribute), true).FirstOrDefault() as AuthorityAttribute;
            var resources = authority?.References?
                  .Select(_ => _.StartsWith("Controllers.") ? resource.Remove(resource.IndexOf("Controllers.")) + _ : _.StartsWith(".") ? resource.Remove(resource.LastIndexOf(".")) + _ : null)
                  .Where(_ => _ != null)
                  .ToArray();

            if (!Membership.AuthorizedAny(filterContext.HttpContext.User, resource, resources))
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                return;
            }
        }
    }
}
