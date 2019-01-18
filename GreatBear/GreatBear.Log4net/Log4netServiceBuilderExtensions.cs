using GreatBear.Core;
using GreatBear.Core.Dependency;
using Microsoft.Extensions.Logging;
using System;

namespace GreatBear.Log4net
{
    public static class Log4netServiceBuilderExtensions
    {
        /// <summary>
        /// Use Dapper as the Orm framework
        /// </summary>
        public static ServicesBuilderOptions AddLog4net(this ServicesBuilderOptions builder)
        {
            builder.IocRegister.Register(typeof(ILoggerFactory), typeof(LoggerFactory),
                lifeStyle: DependencyLifeStyle.Singleton);
            builder.IocRegister.Register<ILogger, Log4NetLogger>(lifeStyle: DependencyLifeStyle.Singleton);
            return builder;
        }
    }
}
