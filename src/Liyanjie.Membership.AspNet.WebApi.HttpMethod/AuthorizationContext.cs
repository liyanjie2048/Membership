using System.Web.Http.Controllers;
using Liyanjie.Membership.Core;

namespace Liyanjie.Membership.AspNet.WebApi.HttpMethod
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthorizationContext : IAuthorizationContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        public AuthorizationContext(HttpActionContext actionContext)
        {
            ActionContext = actionContext;
        }

        /// <summary>
        /// 
        /// </summary>
        public readonly HttpActionContext ActionContext;
    }
}
