using Liyanjie.Membership.Core;
using Microsoft.AspNetCore.Mvc;

namespace Liyanjie.Membership.Sample.AspNetCore.Mvc.Controllers
{
    [AuthorityGroup("根")]
    public abstract class ControllerBase : Controller
    {
    }
}
