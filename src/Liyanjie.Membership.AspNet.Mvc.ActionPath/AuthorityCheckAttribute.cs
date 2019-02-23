using System.Linq;
using System.Net;
using System.Web.Mvc;
using Liyanjie.Membership.Core;

namespace Liyanjie.Membership.AspNet.Mvc.ActionPath
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class AuthorityCheckAttribute : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        protected abstract IMembership<AuthorityProvider> Membership { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public virtual void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            var actionDescriptor = filterContext.ActionDescriptor;
            if (actionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any() || actionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any())
                return;

            if (Membership.IsSuperUser(new AuthorizationContext(filterContext.HttpContext)))
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

            if (!Membership.AuthorizedAny(new AuthorizationContext(filterContext.HttpContext), resource, resources))
            {
                filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Forbidden);
                return;
            }
        }
    }
}
