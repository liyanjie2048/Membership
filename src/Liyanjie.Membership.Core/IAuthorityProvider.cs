using System.Collections.Generic;

namespace Liyanjie.Membership.Core
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
        IEnumerable<IAuthority> GetAuthorities();
    }
}
