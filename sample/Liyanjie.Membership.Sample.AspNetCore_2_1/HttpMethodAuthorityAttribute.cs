using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Liyanjie.Membership.Sample.AspNetCore_2_1
{
    public class HttpMethodAuthorityAttribute : Attribute, IFilterMetadata
    {
    }
}
