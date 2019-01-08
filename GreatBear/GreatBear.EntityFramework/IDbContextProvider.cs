using GreatBear.Core.Dependency;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatBear.EntityFramework
{
    /// <summary>
    /// DbContext provider
    /// </summary>
    public interface IDbContextProvider : ITransientDependency
    {
        /// <summary>
        /// Get data context operation object
        /// </summary>
        DbContextBase GetDbContext();

        /// <summary>
        /// DbContext transaction
        /// </summary>
        IDbContextTransaction DbContextTransaction { get; set; }
    }
}
