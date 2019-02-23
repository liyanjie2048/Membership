﻿using System;
using Liyanjie.Membership.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Liyanjie.Membership.AspNetCore.Mvc.HttpMethod
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
        public static IServiceCollection AddMembership<TAuthorityProvider, TImplementation>(this IServiceCollection sevices, Action<AuthorityOptions<TAuthorityProvider>> authorityOptionsConfigure = null)
            where TAuthorityProvider : class, IAuthorityProvider
            where TImplementation : class, IMembership<TAuthorityProvider>
        {
            sevices.Configure(authorityOptionsConfigure ?? (options => { }));
            sevices.AddTransient<TAuthorityProvider>();
            sevices.AddTransient<IMembership<TAuthorityProvider>, TImplementation>();
            return sevices;
        }
    }
}