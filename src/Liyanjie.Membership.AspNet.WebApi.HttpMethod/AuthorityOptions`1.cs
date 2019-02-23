using System.Web.Http.Description;
using Liyanjie.Membership.Core;

namespace Liyanjie.Membership.AspNet.WebApi.HttpMethod
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthorityOptions<TAuthorityProvider> : AuthorityOptions<TAuthorityProvider, ApiDescription>
    {
        /// <summary>
        /// Default: [GET]=Get; [POST]=Post; [PUT]=Put; [DELETE]=Delete;
        /// </summary>
        public AuthorityDisplay AuthorityDisplay { get; set; } = new AuthorityDisplay();
    }
}
