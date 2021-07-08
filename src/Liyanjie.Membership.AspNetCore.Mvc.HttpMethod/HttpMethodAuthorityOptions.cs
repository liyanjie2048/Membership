using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace Liyanjie.Membership
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpMethodAuthorityOptions : AuthorityOptions<HttpMethodAuthorityProvider, ActionDescriptor>
    {
        /// <summary>
        /// 
        /// </summary>
        public HttpMethodAuthorityOptions()
        {
            Filter = _ => _.FilterDescriptors?.Any(__ => __.Filter is ApiControllerAttribute attribute) == true;
        }

        /// <summary>
        /// Default: [GET]=Get; [POST]=Post; [PUT]=Put; [DELETE]=Delete;
        /// </summary>
        public AuthorityDisplay AuthorityDisplay { get; set; } = new();
    }
}
