using DapperExtensions;
using System;
using System.Data.Common;

namespace GreatBear.Dapper
{
    public interface IDapperProvider
    {
        /// <summary>
        /// Get a database
        /// </summary>
        IDatabase GetDatabase();

        /// <summary>
        /// Database transaction
        /// </summary>
        DbTransaction DbTransaction { get; set; }
    }
}
