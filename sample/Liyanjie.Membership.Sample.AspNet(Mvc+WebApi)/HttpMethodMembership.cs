using System;
using Liyanjie.Membership.AspNet.WebApi.HttpMethod;
using Liyanjie.Membership.Core;

namespace Liyanjie.Membership.Sample.AspNet
{
    public class HttpMethodMembership : Membership<AuthorityProvider>
    {
        public HttpMethodMembership(AuthorityProvider authorityProvider) : base(authorityProvider)
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
