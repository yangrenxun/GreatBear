﻿using DapperExtensions.Mapper;
using DapperExtensions.Sql;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using Dapper;

namespace GreatBear.Dapper
{
    /// <summary>
    /// Dapper options builder
    /// </summary>
    public class DapperOptionsBuilder
    {
        /// <summary>
        /// Sql converter
        /// </summary>
        public SqlDialectBase SqlDialect { get; set; }

        /// <summary>
        /// Default entity and database mapping
        /// </summary>
        public Type DefaultMapper { get; set; }

        /// <summary>
        /// Database connection
        /// </summary>
        public Func<DbConnection> GetDbConnection { get; set; }

        /// <summary>
        /// AutoMapperClass's assembly
        /// </summary>
        public List<Assembly> MapperAssemblies { get; set; }

        /// <inheritdoc />
        public DapperOptionsBuilder()
        {
            MapperAssemblies = new List<Assembly>();
            DefaultMapper = typeof(PluralizedAutoClassMapper<>);
        }

        /// <summary>
        /// Use SqlServer
        /// </summary>
        public void UseSqlServer(string connectionString)
        {
            GetDbConnection = () =>
            {
                return new SqlConnection(connectionString);
            };

            SqlDialect = new SqlServerDialect();
        }

        public void UseMySql(string connectionString)
        {
            GetDbConnection = () =>
            {
                return new MySqlConnection(connectionString);
            };
            SqlDialect = new MySqlDialect();
        }
    }
}
