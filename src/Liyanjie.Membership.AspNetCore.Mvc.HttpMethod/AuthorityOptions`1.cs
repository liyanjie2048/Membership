using Liyanjie.Membership.Core;

using Microsoft.AspNetCore.Mvc.Abstractions;

namespace Liyanjie.Membership.AspNetCore.Mvc.HttpMethod
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthorityOptions<TAuthorityProvider> : AuthorityOptions<TAuthorityProvider, ActionDescriptor>
    {
        /// <summary>
        /// Default: [GET]=Get; [POST]=Post; [PUT]=Put; [DELETE]=Delete;
        /// </summary>
        public AuthorityDisplay AuthorityDisplay { get; set; } = new AuthorityDisplay();
    }
}
