using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Liyanjie.Membership.Sample.AspNetCore.Mvc
{
    public class ActionPathAuthorityAttribute : Attribute, IFilterMetadata
    {
    }
}
