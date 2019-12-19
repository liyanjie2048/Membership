using System;

using Liyanjie.Membership.Core;

using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Liyanjie.Membership.AspNetCore.Mvc.ActionPath
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TAuthorityProvider"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="sevices"></param>
        /// <param name="authorityOptionsConfigure"></param>
        /// <returns></returns>
        public static IServiceCollection AddMembership<TAuthorityProvider, TImplementation>(this IServiceCollection sevices,
            Action<AuthorityOptions<TAuthorityProvider, ActionDescriptor>> authorityOptionsConfigure = null)
            where TAuthorityProvider : class, IAuthorityProvider
            where TImplementation : Membership<TAuthorityProvider>
        {
            if (authorityOptionsConfigure != null)
                sevices.Configure(authorityOptionsConfigure);
            sevices.AddSingleton<TAuthorityProvider>();
            sevices.AddScoped<Membership<TAuthorityProvider>, TImplementation>();
            return sevices;
        }
    }
}
