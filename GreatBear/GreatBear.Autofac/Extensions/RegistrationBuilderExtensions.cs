using Autofac.Builder;
using GreatBear.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatBear.Autofac
{
    /// <summary>
    /// Autofac RegistrationBuilder extensions
    /// </summary>
    public static class RegistrationBuilderExtensions
    {
        /// <summary>
        /// Lifestyle conversion and application
        /// </summary>
        public static void AddLifeStyle<TLimit, TActiatorData, TRegistrationStyle>(
            this IRegistrationBuilder<TLimit, TActiatorData, TRegistrationStyle> registration,
            DependencyLifeStyle lifeStyle)
        {
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    registration.InstancePerDependency();
                    break;
                case DependencyLifeStyle.Scoped:
                    registration.InstancePerLifetimeScope();
                    break;
                case DependencyLifeStyle.Singleton:
                    registration.SingleInstance();
                    break;
                default:
                    throw new ArgumentException(nameof(lifeStyle));
            }
        }
    }
}
