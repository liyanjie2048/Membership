using System;
using System.Linq;
using System.Reflection;

namespace Liyanjie.Membership
{
    /// <summary>
    /// 
    /// </summary>
    public class ActionPathAuthorityOptions : AuthorityOptions<ActionPathAuthorityProvider, MethodInfo>
    {
        /// <summary>
        /// 
        /// </summary>
        public ActionPathAuthorityOptions()
        {
            Filter = _ => _.GetCustomAttributes<AuthorityAttribute>(true).Any();
        }
    }
}
