using GreatBear.Core;
using GreatBear.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatBear.Autofac
{
    /// <summary>
    /// Autofac specific extension methods for <see cref="ServicesBuilderOptions" />.
    /// </summary>
    public static class AutofacServicesBuilderExtension
    {
        /// <summary>
        /// Use Autofac as an injection container
        /// </summary>
        public static ServicesBuilderOptions UseAutofac(this ServicesBuilderOptions builder)
        {
            builder.IocRegister = new AutofacIocRegister();
            builder.IocRegister.Register<IIocResolver, AutofacIocResolver>(DependencyLifeStyle.Transient);
            return builder;
        }
    }
}
