using Microsoft.AspNetCore.Mvc;

namespace Liyanjie.Membership.Sample.AspNetCore.Controllers
{
    [AuthorityGroup("Test")]
    [Route("{controller}/{action}")]
    public class TestController : Controller
    {
        [Authority("列出")]
        public ActionResult List()
        {
            return View();
        }

        [Authority("明细", ".List")]
        public ActionResult Detail()
        {
            return View();
        }

        [Authority("创建", ".List")]
        public ActionResult Create()
        {
            return View();
        }

        [Authority("修改", ".List", ".Detail")]
        public ActionResult Modify()
        {
            return View();
        }

        [Authority("删除", ".List")]
        public ActionResult Delete()
        {
            return View();
        }
    }
}
