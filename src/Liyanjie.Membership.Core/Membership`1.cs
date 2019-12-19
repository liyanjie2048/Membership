using System.Collections.Generic;

namespace Liyanjie.Membership.Core
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Membership<TAuthorityProvider>
        where TAuthorityProvider : IAuthorityProvider
    {
        readonly IAuthorityProvider authorityProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorityProvider"></param>
        public Membership(TAuthorityProvider authorityProvider)
        {
            this.authorityProvider = authorityProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual IEnumerable<IAuthority> GetAuthorities()
        {
            return authorityProvider.GetAuthorities();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorization"></param>
        /// <returns></returns>
        public abstract bool IsSuperUser(IAuthorizationContext authorization);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorization"></param>
        /// <param name="resource"></param>
        /// <param name="resources"></param>
        /// <returns></returns>
        public abstract bool AuthorizedAll(IAuthorizationContext authorization, string resource, params string[] resources);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorization"></param>
        /// <param name="resource"></param>
        /// <param name="resources"></param>
        /// <returns></returns>
        public abstract bool AuthorizedAny(IAuthorizationContext authorization, string resource, params string[] resources);
    }
}
