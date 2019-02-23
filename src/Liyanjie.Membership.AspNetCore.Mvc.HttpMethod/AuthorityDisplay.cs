namespace Liyanjie.Membership.AspNetCore.Mvc.HttpMethod
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

        internal string GetValue(string method)
        {
            switch (method)
            {
                case "GET":
                    return Get;
                case "POST":
                    return Post;
                case "PUT":
                case "PATCH":
                    return Put;
                case "DELETE":
                    return Delete;
                default:
                    return string.Empty;
            }
        }
    }
}
