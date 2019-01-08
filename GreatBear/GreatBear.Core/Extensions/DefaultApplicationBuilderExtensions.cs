using GreatBear.Core.Dependency;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace GreatBear.Core
{
    public static class DefaultApplicationBuilderExtensions
    {
        /// <summary>
        /// Use application framework service
        /// </summary>
        public static void UseDefaultApp(this IApplicationBuilder app, Action<AppBuilderOptions> options = null)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            var iocResolver = app.ApplicationServices.GetService<IIocResolver>();
            var builder = new AppBuilderOptions(iocResolver);
            options?.Invoke(builder);
        }
    }
}
