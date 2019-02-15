using GreatBear.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreatBear.Demo.WebApp
{
    public static class WebAppServicesBuilderExtension
    {
        /// <summary>
        /// Add an SimpleDemoApplication module
        /// </summary>
        public static ServicesBuilderOptions AddWebApp(this ServicesBuilderOptions builder)
        {
            builder.IocRegister.RegisterAssemblyByBasicInterface(typeof(WebAppServicesBuilderExtension).Assembly);
            return builder;
        }
    }
}
