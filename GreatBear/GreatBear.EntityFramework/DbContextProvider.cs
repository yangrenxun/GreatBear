using GreatBear.Core.Dependency;
using GreatBear.Core.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace GreatBear.EntityFramework
{
    /// <inheritdoc />
    public class DbContextProvider : IDbContextProvider
    {
        private class LocalDbContextWapper
        {
            public DbContext DbContext { get; set; }

            public IDbContextTransaction DbContextTransaction { get; set; }
        }

        private readonly IAsyncLocalObjectProvider _asyncLocalObjectProvider;
        private readonly IIocResolver _iocResolver;

        /// <inheritdoc />
        public DbContextProvider(IAsyncLocalObjectProvider asyncLocalObjectProvider, IIocResolver iocResolver)
        {
            _asyncLocalObjectProvider = asyncLocalObjectProvider;
            _iocResolver = iocResolver;
        }

        /// <inheritdoc />
        public DbContext GetDbContext()
        {
            var localDbContext = _asyncLocalObjectProvider.GetCurrent<LocalDbContextWapper>();
            if (localDbContext == null || localDbContext.DbContext == null)
            {
                localDbContext = new LocalDbContextWapper()
                {
                    DbContext = _iocResolver.Resolve<DbContext>()
                };
                _asyncLocalObjectProvider.SetCurrent(localDbContext);
            }
            return localDbContext.DbContext;
        }

        /// <inheritdoc />
        public IDbContextTransaction DbContextTransaction
        {
            get => _asyncLocalObjectProvider.GetCurrent<LocalDbContextWapper>()?.DbContextTransaction;
            set => _asyncLocalObjectProvider.GetCurrent<LocalDbContextWapper>().DbContextTransaction = value;
        }
    }
}
