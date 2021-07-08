using System;
using System.Web.Http;
using System.Web.Http.Description;

namespace Liyanjie.Membership
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpMethodAuthorityOptions : AuthorityOptions<HttpMethodAuthorityProvider, ApiDescription>
    {
        static readonly Type apiControllerType = typeof(ApiController);

        /// <summary>
        /// 
        /// </summary>
        public HttpMethodAuthorityOptions()
        {
            Filter = _ => apiControllerType.IsAssignableFrom(_.ActionDescriptor.ControllerDescriptor.ControllerType);
        }

        /// <summary>
        /// Default: [GET]=Get; [POST]=Post; [PUT]=Put; [DELETE]=Delete;
        /// </summary>
        public AuthorityDisplay AuthorityDisplay { get; set; } = new();
    }
}
