using Liyanjie.Membership.Core;
using Microsoft.AspNetCore.Http;

namespace Liyanjie.Membership.AspNetCore.Mvc.HttpMethod
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
        public AuthorizationContext(HttpContext httpContext)
        {
            HttpContext = httpContext;
        }

        /// <summary>
        /// 
        /// </summary>
        public readonly HttpContext HttpContext;
    }
}
