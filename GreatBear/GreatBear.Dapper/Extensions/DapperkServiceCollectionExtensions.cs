using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatBear.Dapper
{
    /// <summary>
    /// Dapper service extension
    /// </summary>
    public static class DapperkServiceCollectionExtensions
    {
        /// <summary>
        /// Add Dapper's DbConnect connection
        /// </summary>
        public static void AddDapperDbConnection(this IServiceCollection services, Action<DapperOptionsBuilder> options)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            var dapperOptions = new DapperOptionsBuilder();
            options.Invoke(dapperOptions);

            services.AddSingleton(dapperOptions);
        }
    }
}
