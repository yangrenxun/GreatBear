using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatBear.Core
{
    public static class DefaultServiceCollectionExtensions
    {
        /// <summary>
        /// Add application framework service
        /// </summary>
        public static IServiceProvider AddDefaultProvider(this IServiceCollection services, Action<ServicesBuilderOptions> options = null)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            var builder = new ServicesBuilderOptions();
            options?.Invoke(builder);
            return builder.Build(services);
        }
    }
}
