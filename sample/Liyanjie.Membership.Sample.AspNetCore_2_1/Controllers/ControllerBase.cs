using Liyanjie.Membership.Core;
using Microsoft.AspNetCore.Mvc;

namespace Liyanjie.Membership.Sample.AspNetCore_2_1.Controllers
{
    [AuthorityGroup("根")]
    public abstract class ControllerBase : Controller
    {
    }
}
