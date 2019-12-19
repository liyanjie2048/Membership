using System;
using Liyanjie.Membership.AspNetCore.Mvc.ActionPath;
using Liyanjie.Membership.Core;

namespace Liyanjie.Membership.Sample.AspNetCore_2_1
{
    public class ActionPathMembership : Membership<AuthorityProvider>
    {
        public ActionPathMembership(AuthorityProvider authorityProvider) : base(authorityProvider)
        {
        }

        public override bool AuthorizedAll(IAuthorizationContext authorization, string resource, params string[] resources)
        {
            throw new NotImplementedException();
        }

        public override bool AuthorizedAny(IAuthorizationContext authorization, string resource, params string[] resources)
        {
            throw new NotImplementedException();
        }

        public override bool IsSuperUser(IAuthorizationContext authorization)
        {
            throw new NotImplementedException();
        }
    }
}
