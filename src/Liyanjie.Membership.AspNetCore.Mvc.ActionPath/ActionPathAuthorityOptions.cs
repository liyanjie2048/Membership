using System.Linq;

using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace Liyanjie.Membership
{
    /// <summary>
    /// 
    /// </summary>
    public class ActionPathAuthorityOptions : AuthorityOptions<ActionPathAuthorityProvider, ActionDescriptor>
    {
        /// <summary>
        /// 
        /// </summary>
        public ActionPathAuthorityOptions()
        {
            Filter = _ => _.FilterDescriptors?.Any(__ => __.Filter is AuthorityAttribute attribute) == true;
        }
    }
}
