using GreatBear.Core;
using System;

namespace GreatBear.Demo.Application
{
    /// <summary>
    /// Demo application module extension methods for <see cref="ServicesBuilderOptions" />.
    /// </summary>
    public static class DemoApplicationServicesBuilderExtension
    {
        /// <summary>
        /// Add an SimpleDemoApplication module
        /// </summary>
        public static ServicesBuilderOptions AddDemoApplication(this ServicesBuilderOptions builder)
        {
            builder.IocRegister.RegisterAssemblyByBasicInterface(typeof(DemoApplicationServicesBuilderExtension).Assembly);
            return builder;
        }
    }
}
