using System;

namespace Liyanjie.Membership.AspNetCore.Mvc.ActionPath
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class AuthorityAttribute : Attribute
    {
        /// <summary>
        /// 定义权限
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dependences"></param>
        public AuthorityAttribute(string title, params string[] dependences)
        {
            Title = title;
            Dependences = dependences;
        }

        /// <summary>
        /// 定义引用
        /// </summary>
        /// <param name="references"></param>
        public AuthorityAttribute(params string[] references)
        {
            References = references;
            Unlisted = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// 依赖于
        /// </summary>
        public string[] Dependences { get; private set; }

        /// <summary>
        /// 引用于
        /// </summary>
        public string[] References { get; private set; }

        /// <summary>
        /// 不列出
        /// </summary>
        public bool Unlisted { get; private set; }
    }
}
