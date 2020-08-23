using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Liyanjie.Membership.Sample.AspNetCore_2_1.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> List(
            [FromServices] IActionDescriptorCollectionProvider actionDescriptor)
        {
            foreach (var item in actionDescriptor.ActionDescriptors.Items)
            {
                var controllerActionDescriptor = item as ControllerActionDescriptor;
                var controller = controllerActionDescriptor.ControllerTypeInfo.FullName;
                var methods = item.ActionConstraints;
            }
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        public string Detail(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Create([FromBody] string value)
        {
        }

        [HttpPut("{id}")]
        public void Modify(int id, [FromBody] string value)
        {
        }

        [HttpPatch("{id}")]
        public void ModifyXXXX(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
