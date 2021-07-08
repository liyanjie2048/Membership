using System;

namespace Liyanjie.Membership
{
    /// <summary>
    /// 定义权限组
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class AuthorityGroupAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupName">组名称</param>
        public AuthorityGroupAttribute(string groupName)
        {
            GroupName = groupName;
        }

        /// <summary>
        /// 组名称
        /// </summary>
        public string GroupName { get; private set; }
    }
}
