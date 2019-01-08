using GreatBear.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatBear.Core
{
    /// <summary>
    /// Application framework usage
    /// </summary>
    public class AppBuilderOptions
    {
        /// <summary>
        /// Ioc resolver
        /// </summary>
        public readonly IIocResolver IocResolver;

        /// <inheritdoc />
        public AppBuilderOptions(IIocResolver iocResolver)
        {
            IocResolver = iocResolver;
        }
    }
}
