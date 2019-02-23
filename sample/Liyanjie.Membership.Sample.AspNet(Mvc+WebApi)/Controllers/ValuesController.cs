using System.Collections.Generic;
using System.Web.Http;
using Liyanjie.Membership.Core;

namespace Liyanjie.Membership.Sample.AspNet.Controllers
{
    [HttpMethodAuthority]
    [AuthorityGroup("资源")]
    [RoutePrefix("api/[controller]")]
    public class ValuesController : ApiController
    {
        [HttpGet]
        [Route]
        public IEnumerable<string> List()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        [Route("{id}")]
        public string Detail(int id)
        {
            return "value";
        }

        [HttpPost]
        [Route]
        public void Create([FromBody]string value)
        {
        }

        [HttpPut]
        [Route("{id}")]
        public void Modify(int id, [FromBody]string value)
        {
        }

        [HttpPatch]
        [Route("{id}")]
        public void ModifyXXXX(int id, [FromBody]string value)
        {
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(int id)
        {
        }
    }
}
