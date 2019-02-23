using System.Web;
using Liyanjie.Membership.Core;

namespace Liyanjie.Membership.AspNet.Mvc.ActionPath
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthorizationContext : IAuthorizationContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        public AuthorizationContext(HttpContextBase httpContext)
        {
            HttpContext = httpContext;
        }

        /// <summary>
        /// 
        /// </summary>
        public readonly HttpContextBase HttpContext;
    }
}
