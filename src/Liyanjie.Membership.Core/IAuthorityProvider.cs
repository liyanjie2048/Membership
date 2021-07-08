using System.Collections.Generic;

namespace Liyanjie.Membership
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuthorityProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Authority> GetAuthorities();
    }
}
