using DapperExtensions;
using DapperExtensions.Sql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace GreatBear.Dapper
{
    public class DefaultDapperProvider : IDapperProvider
    {
        public IDatabase GetDatabase()
        {
            var dapperOptions = new DapperOptionsBuilder();
            var config = new DapperExtensionsConfiguration(
                dapperOptions.DefaultMapper,
                dapperOptions.MapperAssemblies,
                dapperOptions.SqlDialect);
            var sqlGenerator = new SqlGeneratorImpl(config);
            var database = new Database(dapperOptions.GetDbConnection(), sqlGenerator);
            return database;
        }

        public DbTransaction DbTransaction
        {
            get ;
            set ;
        }
    }
}
