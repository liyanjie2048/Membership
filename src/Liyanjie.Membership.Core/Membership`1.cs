using System.Collections.Generic;
using System.Security.Principal;

namespace Liyanjie.Membership
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
        public virtual IEnumerable<Authority> GetAuthorities()
        {
            return authorityProvider.GetAuthorities();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public abstract bool IsSuperUser(IPrincipal user);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="resource"></param>
        /// <param name="resources"></param>
        /// <returns></returns>
        public abstract bool AuthorizedAll(IPrincipal user, string resource, params string[] resources);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="resource"></param>
        /// <param name="resources"></param>
        /// <returns></returns>
        public abstract bool AuthorizedAny(IPrincipal user, string resource, params string[] resources);
    }
}
