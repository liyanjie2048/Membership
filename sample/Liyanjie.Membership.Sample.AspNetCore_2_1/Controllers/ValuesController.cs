using System.Collections.Generic;
using Liyanjie.Membership.Core;
using Microsoft.AspNetCore.Mvc;

namespace Liyanjie.Membership.Sample.AspNetCore_2_1.Controllers
{
    [HttpMethodAuthority]
    [AuthorityGroup("资源")]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> List()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Detail(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Create([FromBody]string value)
        {
        }

        [HttpPut("{id}")]
        public void Modify(int id, [FromBody]string value)
        {
        }

        [HttpPatch("{id}")]
        public void ModifyXXXX(int id, [FromBody]string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
