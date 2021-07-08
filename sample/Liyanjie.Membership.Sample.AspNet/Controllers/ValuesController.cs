using System.Collections.Generic;
using System.Web.Http;

namespace Liyanjie.Membership.Sample.AspNet.Controllers
{
    [AuthorityGroup("Vaues")]
    //[RoutePrefix("api/[controller]")]
    public class ValuesController : ApiController
    {
        readonly MembershipForMvc membershipMvc;
        readonly MembershipForApi membershipApi;
        public ValuesController(
            MembershipForMvc membershipMvc,
            MembershipForApi membershipApi)
        {
            this.membershipMvc = membershipMvc;
            this.membershipApi = membershipApi;
        }

        [HttpGet]
        //[Route]
        public object List()
        {
            return Json(new
            {
                ActionPathAuthorities = membershipMvc.GetAuthorities(),
                HttpMethodAuthorities = membershipApi.GetAuthorities(),
            });
        }

        [HttpGet]
        //[Route("{id}")]
        public string Detail(int id)
        {
            return "value";
        }

        [HttpPost]
        //[Route]
        public void Create([FromBody] string value)
        {
        }

        [HttpPut]
        //[Route("{id}")]
        public void Modify(int id, [FromBody] string value)
        {
        }

        [HttpPatch]
        //[Route("{id}")]
        public void ModifyXXXX(int id, [FromBody] string value)
        {
        }

        [HttpDelete]
        //[Route("{id}")]
        public void Delete(int id)
        {
        }
    }
}
