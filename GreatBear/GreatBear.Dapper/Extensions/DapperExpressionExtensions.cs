using DapperExtensions;
using GreatBear.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace GreatBear.Dapper
{
    /// <summary>
    /// Dapper specific extension methods for <see cref="Expression" />
    /// </summary>
    internal static class DapperExpressionExtensions
    {
        /// <summary>
        /// Linq expression convert to dapper predicate
        /// </summary>
        public static IPredicate ToPredicateGroup<TEntity, TPrimaryKey>(this Expression<Func<TEntity, bool>> expression)
            where TEntity : class, IEntity<TPrimaryKey>
        {
            var dev = new DapperExpressionVisitor<TEntity, TPrimaryKey>();
            IPredicate pg = dev.Process(expression);

            return pg;
        }
    }
}
