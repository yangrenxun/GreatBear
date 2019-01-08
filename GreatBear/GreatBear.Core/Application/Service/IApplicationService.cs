using GreatBear.Core.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatBear.Core.Application.Service
{
    /// <summary>
    /// Application layer service interface, application services should inherit this interface
    /// </summary>
    public interface IApplicationService : ITransientDependency
    {

    }
}
