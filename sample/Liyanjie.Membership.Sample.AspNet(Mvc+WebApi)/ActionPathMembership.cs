using System;
using System.Security.Principal;

using Liyanjie.Membership.AspNet.Mvc.ActionPath;
using Liyanjie.Membership.Core;

namespace Liyanjie.Membership.Sample.AspNet
{
    public class ActionPathMembership : Membership<AuthorityProvider>
    {
        public ActionPathMembership(AuthorityProvider authorityProvider)
            : base(authorityProvider)
        { }

        public override bool AuthorizedAll(IPrincipal user, string resource, params string[] resources)
        {
            throw new NotImplementedException();
        }

        public override bool AuthorizedAny(IPrincipal user, string resource, params string[] resources)
        {
            throw new NotImplementedException();
        }

        public override bool IsSuperUser(IPrincipal user)
        {
            throw new NotImplementedException();
        }
    }
}
