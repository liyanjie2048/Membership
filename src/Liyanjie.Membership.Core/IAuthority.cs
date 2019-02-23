namespace Liyanjie.Membership.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuthority
    {
        /// <summary>
        /// 资源
        /// </summary>
        string Resource { get; }

        /// <summary>
        /// 分组
        /// </summary>
        string Group { get; }

        /// <summary>
        /// 标题
        /// </summary>
        string Title { get; }

        /// <summary>
        /// 依赖
        /// </summary>
        string[] Dependences { get; }
    }
}
