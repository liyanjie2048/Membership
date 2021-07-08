using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Liyanjie.Membership
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class CheckAuthorityAttribute : FilterAttribute, IAuthorizationFilter
    {
        readonly Membership<ActionPathAuthorityProvider> membership;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="membership"></param>
        public CheckAuthorityAttribute(Membership<ActionPathAuthorityProvider> membership)
        {
            this.membership = membership;
        }

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

            if (membership.IsSuperUser(filterContext.HttpContext.User))
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

            if (!membership.AuthorizedAny(filterContext.HttpContext.User, resource, resources))
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                return;
            }
        }
    }
}
