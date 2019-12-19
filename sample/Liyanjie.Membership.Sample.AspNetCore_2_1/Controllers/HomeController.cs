using System.Collections.Generic;
using Liyanjie.Membership.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Liyanjie.Membership.Sample.AspNetCore_2_1.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public IActionResult Index(
            [FromServices]IMembership<Liyanjie.Membership.AspNetCore.Mvc.ActionPath.AuthorityProvider> actionPathMembership,
            [FromServices]IMembership<Liyanjie.Membership.AspNetCore.Mvc.HttpMethod.AuthorityProvider> httpMethodMembership
            )
        {
            return View(new Dictionary<string, IEnumerable<IAuthority>>
            {
                ["ActionPath"] = actionPathMembership.GetAuthorities(),
                ["HttpMethod"] = httpMethodMembership.GetAuthorities(),
            });
        }
    }
}
