using Liyanjie.Membership.Core;

namespace Liyanjie.Membership.AspNet.Mvc.ActionPath
{
    internal class Authority : IAuthority
    {
        public string Resource { get; set; }

        public string Group { get; set; }

        public string Title { get; set; }

        public string[] Dependences { get; set; }
    }
}
