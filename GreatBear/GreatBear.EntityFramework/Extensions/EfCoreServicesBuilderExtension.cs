using GreatBear.Core;
using GreatBear.Core.Dependency;
using GreatBear.Core.Domain.Repositories;
using System;

namespace GreatBear.EntityFramework
{
    /// <summary>
    /// EfCore specific extension methods for <see cref="ServicesBuilderOptions" />.
    /// </summary>
    public static class EfCoreServicesBuilderExtension
    {
        /// <summary>
        /// Use EfCore Module
        /// </summary>
        public static ServicesBuilderOptions UseEfCore(this ServicesBuilderOptions builder)
        {
            builder.IocRegister.Register(
                typeof(IRepository<,>), typeof(EfRepositoryBase<,>),
                lifeStyle: DependencyLifeStyle.Transient);
            builder.IocRegister.RegisterAssemblyByBasicInterface(typeof(EfCoreServicesBuilderExtension).Assembly);
            return builder;
        }
    }
}
