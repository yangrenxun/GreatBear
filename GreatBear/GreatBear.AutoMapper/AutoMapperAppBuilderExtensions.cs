using AutoMapper;
using GreatBear.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatBear.AutoMapper
{
    /// <summary>
    /// AutoMapper specific extension methods for <see cref="AppBuilderOptions" />.
    /// </summary>
    public static class AutoMapperAppBuilderExtensions
    {
        /// <summary>
        /// Use AutoMapper
        /// </summary>
        public static AppBuilderOptions UseAutoMapper(this AppBuilderOptions builder, Action<IMapperConfigurationExpression> config = null)
        {
            Mapper.Initialize(
                options =>
                {
                    options.ValidateInlineMaps = false;
                    config?.Invoke(options);
                });
            return builder;
        }
    }
}
