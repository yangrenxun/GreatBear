using GreatBear.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GreatBear.Core.Domain.Repositories
{
    public abstract class AbsRepositoryBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        #region sync

        #region select

        /// <summary>
        /// Finds an entity with the given primary key values, and throws an exception If this entity does not exist.
        /// </summary>
        public abstract TEntity Get(TPrimaryKey id);

        /// <summary>
        /// Returns the only element of a sequence, and throws an exception if there is not exactly one element in the sequence.
        /// </summary>
        public abstract TEntity Single(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Returns the first element of a sequence that satisfies a specified condition or a default value if no such element is found.
        /// </summary>
        public abstract TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Get entity collections based on criteria
        /// </summary>
        public abstract List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public abstract IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> predicate = null);
        #endregion

        #region Insert

        /// <summary>
        /// Add an entity
        /// </summary>
        public abstract TEntity Insert(TEntity entity);

        #endregion

        #region Update
        /// <summary>
        /// Update an entity
        /// </summary>
        public abstract TEntity Update(TEntity entity);

        #endregion

        #region Delete

        /// <summary>
        /// Delete an entity by id
        /// </summary>
        public abstract void Delete(TPrimaryKey id);

        /// <summary>
        /// Delete an entity
        /// </summary>
        public abstract void Delete(TEntity entity);

        /// <summary>
        /// Delete entities under the conditions
        /// </summary>
        public abstract void Delete(Expression<Func<TEntity, bool>> predicate = null);

        #endregion

        #region Aggregates

        /// <summary>
        /// Get total number of elements in a sequence.
        /// </summary>
        public abstract long Count(Expression<Func<TEntity, bool>> predicate = null);

        #endregion

        #endregion

        #region Select

        /// <inheritdoc />
        public abstract Task<TEntity> GetAsync(TPrimaryKey id);
        /// <inheritdoc />
        public abstract Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate);
        /// <inheritdoc />
        public abstract Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        /// <inheritdoc />
        public abstract Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null);
        #endregion

        #region Insert

        /// <inheritdoc />
        public Task<TEntity> InsertAsync(TEntity entity)
        {
            if (entity is IHasCreationTime && (entity as IHasCreationTime).CreationTime == default(DateTime))
            {
                (entity as IHasCreationTime).CreationTime = DateTime.Now;
            }
            return InsertEntityAsync(entity);
        }

        /// <inheritdoc />
        public abstract Task<TEntity> InsertEntityAsync(TEntity entity);

        #endregion

        #region Update

        /// <inheritdoc />
        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity is IHasModificationTime)
            {
                (entity as IHasModificationTime).LastModificationTime = DateTime.Now;
            }
            return UpdateEntityAsync(entity);
        }

        /// <inheritdoc />
        public abstract Task<TEntity> UpdateEntityAsync(TEntity entity);

        #endregion

        #region Delete

        /// <inheritdoc />
        public async Task DeleteAsync(TPrimaryKey id)
        {
            var entity = await GetAsync(id);
            await DeleteAsync(entity);
        }

        /// <inheritdoc />
        public virtual async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            foreach (var entity in await GetListAsync(predicate))
            {
                await DeleteAsync(entity);
            }
        }

        /// <inheritdoc />
        public Task DeleteAsync(TEntity entity)
        {
            if (entity is ISoftDelete)
            {
                (entity as ISoftDelete).IsDeleted = true;
                if (entity is IHasDeletionTime)
                {
                    (entity as IHasDeletionTime).DeletionTime = DateTime.Now;
                }
                return UpdateEntityAsync(entity);
            }
            else
            {
                return DeleteEntityAsync(entity);
            }
        }

        /// <inheritdoc />
        public abstract Task DeleteEntityAsync(TEntity entity);

        #endregion

        #region Aggregates

        /// <inheritdoc />
        public abstract Task<long> CountAsync(Expression<Func<TEntity, bool>> predicate = null);

        #endregion
    }
}
