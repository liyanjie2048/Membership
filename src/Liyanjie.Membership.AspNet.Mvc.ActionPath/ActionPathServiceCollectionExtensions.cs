using System;

using Microsoft.Extensions.DependencyInjection;

namespace Liyanjie.Membership
{
    /// <summary>
    /// 
    /// </summary>
    public static class ActionPathServiceCollectionExtensions
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
            Action<ActionPathAuthorityOptions> authorityOptionsConfigure = null)
            where TMembershipImplementation : Membership<ActionPathAuthorityProvider>
            where TAuthorityProvider : ActionPathAuthorityProvider
        {
            if (authorityOptionsConfigure != null)
                sevices.Configure(authorityOptionsConfigure);
            sevices.AddScoped<TMembershipImplementation>();
            sevices.AddScoped<Membership<ActionPathAuthorityProvider>, TMembershipImplementation>();
            sevices.AddSingleton<ActionPathAuthorityProvider, TAuthorityProvider>();
            return sevices;
        }
    }
}
