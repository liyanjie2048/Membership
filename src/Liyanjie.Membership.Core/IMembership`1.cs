using System.Collections.Generic;

namespace Liyanjie.Membership.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMembership<TAuthorityProvider> where TAuthorityProvider : IAuthorityProvider
    {
        /// <summary>
        /// 所有
        /// </summary>
        IEnumerable<IAuthority> GetAuthorities();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorization"></param>
        /// <returns></returns>
        bool IsSuperUser(IAuthorizationContext authorization);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorization"></param>
        /// <param name="resource"></param>
        /// <param name="resources"></param>
        /// <returns></returns>
        bool AuthorizedAll(IAuthorizationContext authorization, string resource, params string[] resources);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorization"></param>
        /// <param name="resource"></param>
        /// <param name="resources"></param>
        /// <returns></returns>
        bool AuthorizedAny(IAuthorizationContext authorization, string resource, params string[] resources);
    }
}
