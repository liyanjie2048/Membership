using System;
using System.Security.Principal;

using Liyanjie.Membership.AspNet.WebApi.HttpMethod;
using Liyanjie.Membership.Core;

namespace Liyanjie.Membership.Sample.AspNet
{
    public class HttpMethodMembership : Membership<AuthorityProvider>
    {
        public HttpMethodMembership(AuthorityProvider authorityProvider)
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
