using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatBear.Core.Dependency
{
    /// <summary>
    /// Base class of interceptor
    /// </summary>
    public abstract class InterceptorBase : IInterceptor
    {
        /// <summary>
        /// Intercept execution method
        /// </summary>
        public abstract void Intercept(IInvocation invocation);
    }
}
