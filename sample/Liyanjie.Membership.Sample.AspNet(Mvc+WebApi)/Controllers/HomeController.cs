using System.Collections.Generic;
using System.Web.Mvc;
using Liyanjie.Membership.Core;

namespace Liyanjie.Membership.Sample.AspNet.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new Dictionary<string, IEnumerable<IAuthority>>
            {
                ["ActionPath"] = Global.ActionPathMembership?.GetAuthorities(),
                ["HttpMethod"] = Global.HttpMethodMembership?.GetAuthorities(),
            });
        }
    }
}
