using Liyanjie.Membership.AspNetCore.Mvc.ActionPath;
using Liyanjie.Membership.Core;
using Microsoft.AspNetCore.Mvc;

namespace Liyanjie.Membership.Sample.AspNetCore.Mvc.Controllers
{
    [ActionPathAuthority]
    [AuthorityGroup("测试")]
    public class TestController : ControllerBase
    {
        [Authority("列出")]
        public IActionResult List()
        {
            return View();
        }

        [Authority("明细", ".List")]
        public IActionResult Detail()
        {
            return View();
        }

        [Authority("创建", ".List")]
        public IActionResult Create()
        {
            return View();
        }

        [Authority("修改", ".List,.Detail")]
        public IActionResult Modify()
        {
            return View();
        }

        [Authority("删除", ".List")]
        public IActionResult Delete()
        {
            return View();
        }
    }
}
