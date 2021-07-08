using System;
using System.Security.Principal;

namespace Liyanjie.Membership.Sample.AspNetCore
{
    public class MembershipForApi : Membership<HttpMethodAuthorityProvider>
    {
        public MembershipForApi(HttpMethodAuthorityProvider provider)
            : base(provider) { }

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
