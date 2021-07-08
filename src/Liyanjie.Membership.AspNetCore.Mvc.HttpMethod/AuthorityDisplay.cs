namespace Liyanjie.Membership
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthorityDisplay
    {
        /// <summary>
        /// 
        /// </summary>
        public string Get { get; set; } = nameof(Get);

        /// <summary>
        /// 
        /// </summary>
        public string Post { get; set; } = nameof(Post);

        /// <summary>
        /// 
        /// </summary>
        public string Put { get; set; } = nameof(Put);

        /// <summary>
        /// 
        /// </summary>
        public string Delete { get; set; } = nameof(Delete);

        internal string GetValue(string method) => method switch
        {
            "GET" => Get,
            "POST" => Post,
            "PUT" => Put,
            "DELETE" => Delete,
            _ => string.Empty,
        };
    }
}
