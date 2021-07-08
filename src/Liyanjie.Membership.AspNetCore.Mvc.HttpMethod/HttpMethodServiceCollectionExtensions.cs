using System;

using Microsoft.Extensions.DependencyInjection;

namespace Liyanjie.Membership
{
    /// <summary>
    /// 
    /// </summary>
    public static class HttpMethodServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TMembershipImplementation"></typeparam>
        /// <typeparam name="TAuthorityProvider"></typeparam>
        /// <param name="sevices"></param>
        /// <param name="authorityOptionsConfigure"></param>
        /// <returns></returns>
        public static IServiceCollection AddMembership<TMembershipImplementation, TAuthorityProvider>(this IServiceCollection sevices,
            Action<HttpMethodAuthorityOptions> authorityOptionsConfigure = null)
            where TAuthorityProvider : HttpMethodAuthorityProvider
            where TMembershipImplementation : Membership<HttpMethodAuthorityProvider>
        {
            if (authorityOptionsConfigure != null)
                sevices.Configure(authorityOptionsConfigure);
            sevices.AddScoped<TMembershipImplementation>();
            sevices.AddScoped<Membership<HttpMethodAuthorityProvider>, TMembershipImplementation>();
            sevices.AddSingleton<HttpMethodAuthorityProvider, TAuthorityProvider>();
            return sevices;
        }
    }
}
