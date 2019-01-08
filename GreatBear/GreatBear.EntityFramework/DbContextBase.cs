using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatBear.EntityFramework
{
    /// <inheritdoc />
    public abstract class DbContextBase : DbContext
    {
        /// <summary>
        /// Mark if DbContext has been released
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <inheritdoc />
        public DbContextBase(DbContextOptions options) : base(options)
        {

        }

        /// <inheritdoc />
        public override void Dispose()
        {
            IsDisposed = true;
            base.Dispose();
        }
    }
}
