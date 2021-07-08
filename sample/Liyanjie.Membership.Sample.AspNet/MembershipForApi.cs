using System;
using System.Security.Principal;

namespace Liyanjie.Membership.Sample.AspNet
{
    public class MembershipForApi : Membership<HttpMethodAuthorityProvider>
    {
        public MembershipForApi(HttpMethodAuthorityProvider authorityProvider)
            : base(authorityProvider) { }

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
