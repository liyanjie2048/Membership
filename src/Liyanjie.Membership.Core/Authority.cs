namespace Liyanjie.Membership
{
    /// <summary>
    /// 
    /// </summary>
    public class Authority
    {
        /// <summary>
        /// 
        /// </summary>
        public string Resource { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string[] Dependences { get; set; }
    }
}
