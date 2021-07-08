using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Liyanjie.Membership.Sample.AspNetCore.Controllers
{
    [AllowAnonymous]
    [Route("{controller}/{action}")]
    public class HomeController : Controller
    {
        readonly MembershipForMvc membershipMvc;
        readonly MembershipForApi membershipApi;
        public HomeController(
            MembershipForMvc membershipMvc,
            MembershipForApi membershipApi)
        {
            this.membershipMvc = membershipMvc;
            this.membershipApi = membershipApi;
        }

        public ActionResult<object> Index()
        {
            return new
            {
                ActionPathAuthorities = membershipMvc.GetAuthorities(),
                HttpMethodAuthorities = membershipApi.GetAuthorities(),
            };
        }
    }
}
