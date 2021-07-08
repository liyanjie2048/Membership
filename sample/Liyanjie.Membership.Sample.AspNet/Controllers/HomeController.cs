using System.Collections.Generic;
using System.Web.Mvc;

namespace Liyanjie.Membership.Sample.AspNet.Controllers
{
    [AllowAnonymous]
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

        public ActionResult Index()
        {
            return Json(new
            {
                ActionPathAuthorities = membershipMvc.GetAuthorities(),
                HttpMethodAuthorities = membershipApi.GetAuthorities(),
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
