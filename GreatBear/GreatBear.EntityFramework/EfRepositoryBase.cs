using GreatBear.Core.Domain.Entities;
using GreatBear.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GreatBear.EntityFramework
{
    /// <inheritdoc />
    public class EfRepositoryBase<TEntity, TPrimaryKey> : AbsRepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        private readonly IDbContextProvider _dbContextProvider;

        /// <inheritdoc />
        public EfRepositoryBase(IDbContextProvider dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        /// <summary>
        /// Gets EF DbContext object.
        /// </summary>
        public virtual DbContext DbContext => _dbContextProvider.GetDbContext();

        /// <summary>
        /// Gets DbSet for given entity.
        /// </summary>
        public virtual DbSet<TEntity> Table => DbContext.Set<TEntity>();
        #region sync

        #region select

        /// <summary>
        /// Finds an entity with the given primary key values, and throws an exception If this entity does not exist.
        /// </summary>
        public override TEntity Get(TPrimaryKey id)
        {
            var entity = Table.Find(id);
            if (entity == null)
            {
                throw new Exception($"There is no such an entity. Entity type: {typeof(TEntity).FullName}, id: {id}");
            }
            return entity;
        }

        /// <summary>
        /// Returns the only element of a sequence, and throws an exception if there is not exactly one element in the sequence.
        /// </summary>
        public override TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return Table.Single(predicate);
        }
        /// <summary>
        /// Returns the first element of a sequence that satisfies a specified condition or a default value if no such element is found.
        /// </summary>
        public override TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Table.FirstOrDefault(predicate);
        }
        /// <summary>
        /// Get entity collections based on criteria
        /// </summary>
        public override List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = Table.AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query.ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public override IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = Table.AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query;
        }
        #endregion

        #region Insert

        /// <summary>
        /// Add an entity
        /// </summary>
        public override TEntity Insert(TEntity entity)
        {
            var entityEntry = Table.Add(entity);
            DbContext.SaveChanges();
            return entityEntry.Entity;
        }

        #endregion

        #region Update
        /// <summary>
        /// Update an entity
        /// </summary>
        public override TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            var entityEntry = DbContext.Entry(entity);
            if (!DbContext.ChangeTracker.AutoDetectChangesEnabled)
            {
                entityEntry.State = EntityState.Modified;
            }
            DbContext.SaveChanges();
            return entity;
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete an entity by id
        /// </summary>
        public override void Delete(TPrimaryKey id)
        {
            var entity = Get(id);
            Delete(entity);
        }

        /// <summary>
        /// Delete an entity
        /// </summary>
        public override void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
            DbContext.SaveChanges();
        }

        /// <summary>
        /// Delete entities under the conditions
        /// </summary>
        public override void Delete(Expression<Func<TEntity, bool>> predicate = null)
        {
            foreach (var entity in GetList(predicate))
            {
                Delete(entity);
            }
        }

        #endregion

        #region Aggregates

        /// <summary>
        /// Get total number of elements in a sequence.
        /// </summary>
        public override long Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = Table.AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query.LongCount();
        }

        #endregion

        #endregion
        #region Select

        /// <inheritdoc />
        public override async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            var entity = await Table.FindAsync(id);
            if (entity == null)
            {
                throw new Exception($"There is no such an entity. Entity type: {typeof(TEntity).FullName}, id: {id}");
            }
            return entity;
        }

        /// <inheritdoc />
        public override async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.SingleAsync(predicate);
        }

        /// <inheritdoc />
        public override async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await Table.FirstOrDefaultAsync(predicate);
        }

        /// <inheritdoc />
        public override async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = Table.AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return await query.ToListAsync();
        }
        #endregion

        #region Insert

        /// <inheritdoc />
        public override async Task<TEntity> InsertEntityAsync(TEntity entity)
        {
            var entityEntry = await Table.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entityEntry.Entity;
        }

        #endregion

        #region Update

        /// <inheritdoc />
        public override async Task<TEntity> UpdateEntityAsync(TEntity entity)
        {
            AttachIfNot(entity);
            var entityEntry = DbContext.Entry(entity);
            if (!DbContext.ChangeTracker.AutoDetectChangesEnabled)
            {
                entityEntry.State = EntityState.Modified;
            }
            await DbContext.SaveChangesAsync();
            return entity;
        }

        #endregion

        #region Delete

        /// <inheritdoc />
        public override async Task DeleteEntityAsync(TEntity entity)
        {
            AttachIfNot(entity);
            Table.Remove(entity);
            await DbContext.SaveChangesAsync();
        }

        #endregion

        #region Aggregates

        /// <inheritdoc />
        public override async Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            var query = Table.AsQueryable();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return await query.LongCountAsync();
        }

        #endregion

        /// <inheritdoc />
        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = DbContext.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            Table.Attach(entity);
        }
    }
}
