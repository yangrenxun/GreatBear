using DapperExtensions.Mapper;
using GreatBear.Core;
using GreatBear.Core.Dependency;
using GreatBear.Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatBear.Dapper
{
    /// <summary>
    /// Dapper specific extension methods for <see cref="ServicesBuilderOptions" />.
    /// </summary>
    public static class DapperServicesBuilderExtension
    {
        /// <summary>
        /// Use Dapper as the Orm framework
        /// </summary>
        public static ServicesBuilderOptions UseDapper(this ServicesBuilderOptions builder)
        {
            builder.IocRegister.Register(
                typeof(IRepository<,>), typeof(DapperRepositoryBase<,>),
                lifeStyle: DependencyLifeStyle.Transient);
            builder.IocRegister.RegisterAssemblyByBasicInterface(typeof(DapperServicesBuilderExtension).Assembly);
            DapperExtensions.DapperExtensions.DefaultMapper = typeof(PluralizedAutoClassMapper<>);
            return builder;
        }
    }
}
